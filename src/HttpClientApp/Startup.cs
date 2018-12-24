using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Http;
using Polly;
using System.Net.Http;
using System.Net;
using Polly.Timeout;
using Polly.Extensions.Http;

namespace HttpClientApp
{
    public class Startup
    {
        private readonly ILogger _logger;
       
        public Startup(IConfiguration configuration,ILogger<Startup> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //https://github.com/App-vNext/Polly/wiki/Polly-Roadmap

            //自定义异常策略(只要不是200正常返回都进行重试）
            var retryPolicy = Policy.Handle<HttpRequestException>()
                .OrResult<HttpResponseMessage>(response => response.StatusCode != HttpStatusCode.OK )
                .WaitAndRetryAsync(new[]
                {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(2),
                    TimeSpan.FromSeconds(4)
                }, 
                onRetry: (outcome, timespan, retryAttempt, context) =>
                {                   
                      _logger.LogWarning("Delaying for {delay}ms, then making retry {retry}.", timespan.TotalMilliseconds, retryAttempt);
                });
            #region 定义超时策略
            var timeOutRetryPolicy = HttpPolicyExtensions
                .HandleTransientHttpError()
                .Or<TimeoutRejectedException>() // thrown by Polly's TimeoutPolicy if the inner call times out
                .WaitAndRetryAsync(new[]
                    {
                        TimeSpan.FromSeconds(1),
                        TimeSpan.FromSeconds(5),
                        TimeSpan.FromSeconds(10)
                    });
            var timeoutPolicy = Policy.TimeoutAsync<HttpResponseMessage>(10); // Timeout for an individual try 每次重试超时的时间

            #endregion


            services.AddHttpClient("values", option =>
            {
                option.BaseAddress = new Uri("http://localhost:5000");
                option.DefaultRequestHeaders.Add("Accept", "application/vnd.github.v3+json");
                option.Timeout = TimeSpan.FromSeconds(60); // Overall timeout across all tries  所有重试时间累加起来的超时
            })

            //遇到错误进行重试策略
            //Network failures (System.Net.Http.HttpRequestException)
            //HTTP 5XX status codes(server errors)
            //HTTP 408 status code(request timeout)
            //.AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[]
            //    {
            //        TimeSpan.FromSeconds(1),
            //        TimeSpan.FromSeconds(5),
            //        TimeSpan.FromSeconds(10)
            //    }))

            //.AddPolicyHandler(retryPolicy);

            .AddPolicyHandler(timeOutRetryPolicy)
            .AddPolicyHandler(timeoutPolicy);

            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
