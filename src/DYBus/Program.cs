﻿using DotnetSpider.Core;
using DotnetSpider.Core.Downloader;
using DotnetSpider.Core.Pipeline;
using DotnetSpider.Core.Scheduler;
using DotnetSpider.Downloader;
using DYBus.ProcessorAndPipeline;
using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using DYBus.Data;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;
using Microsoft.EntityFrameworkCore.Design;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using System.Collections.Generic;
using DYBus.Models;
using Polly.Extensions.Http;
using Polly;

namespace DYBus
{
    class Program : IDesignTimeDbContextFactory<BusDbContent>
    {
        static void Main(string[] args)
        {
            Program pro = new Program();
            object lockobj = new object();
            var configurationBuilder = new ConfigurationBuilder()
                                   .SetBasePath(Directory.GetCurrentDirectory())
                                   .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfiguration configuration = configurationBuilder.Build();

            //依赖注入
            var service = new ServiceCollection();
            service.AddDbContext<BusDbContent>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });


            //添加Http
            service.AddHttpClient("DYBUS", option =>
            {
                option.Timeout = TimeSpan.FromSeconds(30);
            });
            //.AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[] {
            //            TimeSpan.FromSeconds(1),
            //            TimeSpan.FromSeconds(5),
            //            TimeSpan.FromSeconds(10)})

            //).AddTransientHttpErrorPolicy(builder => builder.CircuitBreakerAsync(3, TimeSpan.FromSeconds(30)));




            var serviceProvider = service.BuildServiceProvider();
            //获取所有车站信息
            //DYBusStationHandler.Run();
            Console.WriteLine("启动程序,开始获取当前公交车信息");
            var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
            var httpClient = httpClientFactory.CreateClient("DYBUS");

            Dictionary<string, List<BusRunTimeInfo>> dic = new Dictionary<string, List<BusRunTimeInfo>>();
            foreach (var item in new string[] { "6" })
            {


                //记录当前公交路线
                var busline = item;
                dic.Add(busline, new List<BusRunTimeInfo>() { });
                Task.Factory.StartNew(async () =>
                {
                    Console.WriteLine($"{busline} 启动获取数据----{dic[busline].Count}");
                    while (true)
                    {
                        try
                        {
                            if (dic[busline].Count == 30)
                            {
                                lock (lockobj)
                                {
                                    Console.WriteLine($"{busline} 开始插入数据");
                                    var _dbContext = serviceProvider.GetRequiredService<BusDbContent>();
                                    _dbContext.AddRange(dic[busline]);
                                    _dbContext.SaveChanges();
                                    Console.WriteLine($"{busline} 开始插入数据");
                                    dic[busline].Clear();
                                }
                            }
                            var result = await httpClient.GetStringAsync($"http://wapapp.dy4g.cn/bus/auto/test.php?t=busdb&busline={busline}");
                           
                            if (!string.IsNullOrEmpty(result))
                            {
                                if (result.Length > 10)
                                {
                                    var listBUSNO = result.Split(';');
                                    foreach (var busnoItem in listBUSNO)
                                    {
                                        int start = busnoItem.IndexOf("(");
                                        int end = busnoItem.IndexOf(")");
                                        if (end - start > 1)
                                        {
                                            var usefulInfo = busnoItem.Substring(start + 1, end - start - 1);
                                            var infoArray = usefulInfo.Split(",");
                                            if (infoArray.Length == 6)
                                            {
                                                dic[busline].Add(new BusRunTimeInfo
                                                {
                                                    RoadNum = busline,
                                                    BusCarNo = infoArray[0],
                                                    BusRunDirection = infoArray[1],
                                                    BusStatus = infoArray[2],
                                                    BeforeStationNo = infoArray[3],
                                                    AheadStationNo = infoArray[4],
                                                    IsAtFinalStop = infoArray[5],
                                                    CreateTime = DateTime.Now
                                                });
                                            }


                                        }

                                    }
                                }
                            }
                            Console.WriteLine($"{busline} ---- {dic[busline].Count}\r\n");
                            Thread.Sleep(3000);
                        }
                        catch (Exception ex)
                        {

                            Console.WriteLine(ex.ToString());
                        }


                    }
                });
            }


            Console.ReadKey();
        }




        public BusDbContent CreateDbContext(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfiguration configuration = configurationBuilder.Build();
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            DbContextOptionsBuilder<BusDbContent> optionsBuilder = new DbContextOptionsBuilder<BusDbContent>()
                .UseSqlServer(connectionString);
            return new BusDbContent(optionsBuilder.Options);
        }
    }
}