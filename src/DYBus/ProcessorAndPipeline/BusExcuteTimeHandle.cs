using DotnetSpider.Core;
using DotnetSpider.Downloader;
using DotnetSpider.Extension;
using DotnetSpider.Extension.Model;
using DotnetSpider.Extension.Pipeline;
using DotnetSpider.Extraction;
using DotnetSpider.Extraction.Model;
using DotnetSpider.Extraction.Model.Attribute;
using System;
using System.Collections.Generic;
using System.Text;

namespace DYBus.ProcessorAndPipeline
{
    /// <summary>
    /// 获取Bus运行时间
    /// </summary>
    public class BusExcuteTimeHandle
    {
        public static void Run()
        {
            DyBusRunTimeNodeSpider spider = new DyBusRunTimeNodeSpider();
            spider.Run();
        }

        /// <summary>
        /// 获取时间节点
        /// </summary>
        private class DyBusRunTimeNodeSpider:EntitySpider
        {
            public DyBusRunTimeNodeSpider() { }

            protected override void OnInit(params string[] arguments)
            {
                string line = "1";
                
              
                AddRequest($"http://wapapp.dy4g.cn/bus/auto/test.php?t=busdb&busline={line}", new Dictionary<string, dynamic> { { "Keyword", $"{line}路线" } });

                Downloader.AddAfterDownloadCompleteHandler(new BusResultHandler());                
                AddEntityType<BusResultEntity>(new BusDataHanlder());  
                AddPipeline(new ConsoleEntityPipeline());
            }            
        }

        
        public class BusResultHandler : AfterDownloadCompleteHandler
        {

            public BusResultHandler() { }
            public override void Handle(ref Response response, IDownloader downloader)
            {
                
                var text = response.Content?.ToString();
                if(string.IsNullOrWhiteSpace(text))
                {
                    return;
                }
                Console.WriteLine(text);
                
                response.Content = "{\"price\":\"10000\"}"; 

            }
        }

        public class BusDataHanlder : IDataHandler
        {
            public void Handle(ref dynamic data, Page page)
            {
                var price = float.Parse(data.price);
                var sold = int.Parse(data.sold);

                if (price >= 100 && price < 5000)
                {
                    if (sold <= 1)
                    {
                        if (!page.SkipTargetRequests)
                        {
                            page.SkipTargetRequests = true;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else if (price < 100)
                {
                    if (sold <= 5)
                    {
                        if (!page.SkipTargetRequests)
                        {
                            page.SkipTargetRequests = true;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    if (sold == 0)
                    {
                        if (!page.SkipTargetRequests)
                        {
                            page.SkipTargetRequests = true;
                        }
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }


        [Schema("DYBus","BusExcuteTime")]
        [Entity(Expression = "$.[*]", Type = SelectorType.JsonPath)]
        public class BusResultEntity: IBaseEntity
        {
            [Column]
            [Field(Expression = "$.price", Type = SelectorType.JsonPath)]
            public string price { get; set; }
        }


       
    }


}
