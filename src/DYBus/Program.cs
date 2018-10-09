using DotnetSpider.Core;
using DotnetSpider.Core.Downloader;
using DotnetSpider.Core.Pipeline;
using DotnetSpider.Core.Processor;
using DotnetSpider.Core.Scheduler;
using DotnetSpider.Downloader;
using DYBus.ProcessorAndPipeline;
using System;


namespace DYBus
{
    class Program
    {
        static void Main(string[] args)
        {

            //获取所有车站信息
            DYBusStationHandler.Run();
            Console.WriteLine("-----------------------------------------------");


            Console.ReadKey();
        }
    }
}
