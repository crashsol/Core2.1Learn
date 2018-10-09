using DotnetSpider.Core;
using DotnetSpider.Core.Downloader;
using DotnetSpider.Core.Pipeline;
using DotnetSpider.Core.Processor;
using DotnetSpider.Core.Scheduler;
using DotnetSpider.Downloader;
using DYBus.PageProcessors;
using DYBus.Pipelines;
using System;


namespace DYBus
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        private static void BusPageProcessorAndPipeline() 
        {
            // 玄幻小说的首页网址

            string url = "http://www.jjxsw.com/e/action/ListInfo.php?page=0&classid=11&line=10&tempid=3&ph=1&andor=and&qujian=4&orderby=2&myorder=0&totalnum=150";

            Site site = new Site { CycleRetryTimes = 3, SleepTime = 300 };

            site.AddStartUrl(url);

            // 使用 PageProcessor和 Scheduler创建一个爬行器。添加数据处理管道

            Spider spider = Spider.Create(site, new QueueDuplicateRemovedScheduler(), new BusPageProcessor()).AddPipeline(new BusPipeline());

            // 爬虫下载器
            spider.Downloader = new HttpClientDownloader();

            // 爬虫的线程数
            spider.ThreadNum = 4;

            // 当没有其它链接请求时，爬虫的退出时间
            spider.EmptySleepTime = 2000;

            // 启动爬虫
            spider.Run();
        }
    }
}
