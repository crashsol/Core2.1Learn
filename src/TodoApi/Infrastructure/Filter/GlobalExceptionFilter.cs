using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Infrastructure.OperationExcetion;

namespace TodoApi.Infrastructure.Filter
{
    /// <summary>
    /// MVC管道全局异常处理
    /// </summary>
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger _logger;
        private readonly IHostingEnvironment _env;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger,IHostingEnvironment hostingEnvironment)
        {
            _logger = logger;
            _env = hostingEnvironment;
        }

        public void OnException(ExceptionContext context)
        {
            var result = new JsonErrorResponse();

            if(context.Exception.GetType() == typeof(UserOperationException))
            {
                //自定义异常
                result.Message = context.Exception.Message;
                context.Result = new BadRequestObjectResult(result);
            }
            else
            {
                result.Message = "发生了未知的内部错误";
                if(_env.IsDevelopment())
                {
                    //非生产环境，返回错误堆栈信息
                    result.DeveloperMessage = context.Exception.StackTrace;
                }
                context.Result = new InternalServerErrorObjectResult(result);
            }

            //记录错误信息
            _logger.LogError(context.Exception, context.Exception.Message);
            context.ExceptionHandled = true;

        }

        private class InternalServerErrorObjectResult : ObjectResult        {
          

            public InternalServerErrorObjectResult(object result):base(result)
            {
                StatusCode = StatusCodes.Status500InternalServerError;
            }
        }
    }
}
