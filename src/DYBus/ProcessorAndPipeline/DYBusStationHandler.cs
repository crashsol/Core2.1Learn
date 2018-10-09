using DotnetSpider.Extension;
using DotnetSpider.Extension.Model;
using DotnetSpider.Extension.Pipeline;
using DotnetSpider.Extraction;
using DotnetSpider.Extraction.Model.Attribute;
using System;
using System.Collections.Generic;
using System.Text;

namespace DYBus.ProcessorAndPipeline
{
    public class DYBusStationHandler
    {

        public static void Run()
        {
            DYBusStationSpider dYBusStationSpider = new DYBusStationSpider();
            dYBusStationSpider.Run();

        }


        /// <summary>
        /// 定义一个爬虫获取所有车站信息
        /// </summary>

        private class DYBusStationSpider : EntitySpider
        {
            public DYBusStationSpider() { }

            protected override void OnInit(params string[] arguments)
            {
               
                foreach (var item in BusLines.Lines)
                {
                    AddRequest($"http://wapapp.dy4g.cn/bus/auto/test.php?t=linhtml&busline={item}", new Dictionary<string, dynamic> { { "Keyword",$"{item}公交线" } });
                }
            
                AddEntityType<BusStation>();
                AddPipeline(new ConsoleEntityPipeline());
                AddPipeline(new SqlServerEntityPipeline("Server=.\\SQLEXPRESS;uid=sa;pwd=123qwe!@#;Database=DyBus;Trusted_Connection=True;MultipleActiveResultSets=true"));
            }


            /// <summary>
            /// 获取车站信息
            /// </summary>
            [Schema("DYBus", "BusStation")]
            [Entity(Expression = ".//div[@class='busstoplist']/div/p", Type = SelectorType.XPath)]
            class BusStation : BaseEntity
            {
                /// <summary>
                /// 车次信息
                /// </summary>
                [Column]
                [Field(Expression = "Keyword", Type = SelectorType.Enviroment)]
                public string Keyword { get; set; }

                /// <summary>
                /// 车站唯一ID
                /// </summary>
                [Column]
                [Field(Expression = "./@Id")]
                public string BusStationId { get; set; }


                /// <summary>
                /// 车次路线编号
                /// </summary>
                [Column]
                [Field(Expression = "./strong/text()")]
                public string StationNumber { get; set; }

                /// <summary>
                /// 车站名称
                /// </summary>
                [Column]
                [Field(Expression = "./span/text()")]
                public string Name { get; set; }

                /// <summary>
                /// 车站方向
                /// </summary>
                [Column]
                [Field(Expression = "../@class")]
                public string BusDirection { get; set; }

            }

        }
    }
}
