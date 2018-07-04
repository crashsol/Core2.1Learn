using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Infrastructure.OperationExcetion
{

    /// <summary>
    /// 自定义Json错误信息返回
    /// </summary>
    public class JsonErrorResponse
    {
        /// <summary>
        /// 提示消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 开发异常提示
        /// </summary>
        public object DeveloperMessage { get; set; }
    }
}
