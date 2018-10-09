using DotnetSpider.Core;
using DotnetSpider.Core.Processor;
using System;
using System.Collections.Generic;
using System.Text;

namespace DYBus.PageProcessors
{
    /// <summary>
    /// 对获取到的网页，进行解析，解析到的数据，将传到 Pipeline 中，进行处理
    /// </summary>
    class BusPageProcessor : BasePageProcessor
    {
        protected override void Handle(Page page)
        {
            throw new NotImplementedException();
        }
    }
}
