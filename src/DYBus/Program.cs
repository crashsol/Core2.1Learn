using DotnetSpider.Core;
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
using System.Linq;

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
            })
            .AddTransientHttpErrorPolicy(builder => builder.WaitAndRetryAsync(new[] {
                        TimeSpan.FromSeconds(1),
                        TimeSpan.FromSeconds(10),
                        TimeSpan.FromSeconds(20)})

            ).AddTransientHttpErrorPolicy(builder => builder.CircuitBreakerAsync(3, TimeSpan.FromSeconds(60)));




            var serviceProvider = service.BuildServiceProvider();
            //获取所有车站信息
            if(configuration["DownloadBusLineInfo"] == "true")
            {
                DYBusStationHandler.Run();
            }           
            Console.WriteLine("启动程序,开始获取当前公交车信息");
            var httpClientFactory = serviceProvider.GetService<IHttpClientFactory>();
            var httpClient = httpClientFactory.CreateClient("DYBUS");

            Dictionary<string, List<BusRunTimeInfo>> dic = new Dictionary<string, List<BusRunTimeInfo>>();
            foreach (var item in BusLines.Lines)
            {
                //记录当前公交路线
                var busline = item;
                dic.Add(busline, new List<BusRunTimeInfo>() { });
                Task.Factory.StartNew(async () =>
                {
                    Console.WriteLine($"{busline} 启动获取数据----{dic[busline].Count}");
                    while (true)
                    {
                        if (dic[busline].Count > 50)
                        {
                            lock (lockobj)
                            {
                                Console.WriteLine($"{busline} 开始插入数据");
                                var _dbContext = serviceProvider.GetRequiredService<BusDbContent>();
                                _dbContext.AddRange(dic[busline]);
                                _dbContext.SaveChanges();
                                dic[busline].Clear();
                                Console.WriteLine($"{busline} 开始插入数据");                               
                             
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
                                            //如果存在相同的数据就不记录了
                                            if (!dic[busline].Any(b =>
                                                                b.RoadNum == busline &&
                                                                b.BusRunDirection == infoArray[1] &&
                                                                b.BusCarNo == "BUSID_" + infoArray[0] &&
                                                                b.BusStatus == infoArray[2] &&
                                                                b.BeforeStationNo == ("BUSSTOPNO_"+ infoArray[3]) &&
                                                                b.CurrentStationNo == ("BUSSTOPNO_"+infoArray[4]) &&
                                                                b.IsBusStop == infoArray[5]))
                                            {
                                                ///汽车到站
                                                if(!(infoArray[5]=="0" &&infoArray[2]!="1" && infoArray[2]!="5"))
                                                {
                                                    dic[busline].Add(new BusRunTimeInfo
                                                    {
                                                        RoadNum = busline,
                                                        BusCarNo = "BUSID_"+ infoArray[0],
                                                        BusRunDirection = infoArray[1],
                                                        BusStatus = infoArray[2],
                                                        BeforeStationNo = "BUSSTOPNO_" + infoArray[3],
                                                        CurrentStationNo = "BUSSTOPNO_"+ infoArray[4],
                                                        IsBusStop = infoArray[5],
                                                        CreateTime = DateTime.Now
                                                    });
                                                }                                               
                                            }

                                        }
                                    }

                                }
                            }
                        }
                        Console.WriteLine($"{busline}路线当前获取到数据:{dic[busline].Count}");
                        await Task.Delay(3000);
                    }
                });
            }

            Console.WriteLine("正在获取数据....");
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
