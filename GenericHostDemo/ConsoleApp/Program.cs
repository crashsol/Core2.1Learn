using Microsoft.Extensions.Hosting;
using System;
using NLog;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp
{
    class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            var builder = new HostBuilder()
                 .ConfigureLogging(factory =>
                 {

                     factory.AddNLog(new NLogProviderOptions { CaptureMessageProperties = true, CaptureMessageTemplates = true });
                     NLog.LogManager.LoadConfiguration("nlog.config");
                     factory.AddConsole();
                 })
                //host Config
                .ConfigureHostConfiguration(config =>
                {

                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                //添加app Config
                .ConfigureAppConfiguration((hostConext, config) =>
                {
                    var env = hostConext.HostingEnvironment;
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                          .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

                    config.AddEnvironmentVariables();
                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }

                })
                //service
                .ConfigureServices((hostContext, services) =>
                {

                    services.AddOptions();
                    services.Configure<AppSettings>(hostContext.Configuration.GetSection("AppSettings"));

                    //Host Service Inject
                    services.AddHostedService<PrinterHostService>();
                    services.AddHostedService<TimerHostService>();
                });


               await builder.RunConsoleAsync();
        }
    }
}
