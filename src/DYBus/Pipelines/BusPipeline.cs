using DotnetSpider.Core;
using DotnetSpider.Core.Pipeline;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace DYBus.Pipelines
{
    /// <summary>
    /// 对 PageProcessor 解析到的页面数据，进行处理
    /// </summary>
    class BusPipeline : BasePipeline
    {
        public override void Process(IList<ResultItems> resultItems, dynamic sender = null)
        {
            throw new NotImplementedException();
        }
    }
}
