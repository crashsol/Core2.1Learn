using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace TodoApi.Infrastructure.Filter
{
    /// <summary>
    /// 参数统一验证
    /// </summary>
    public class ValidateModelFilter : ActionFilterAttribute
    {

        private readonly ILogger _logger;
        public ValidateModelFilter(ILogger<ValidateModelFilter> logger)
        {
            _logger = logger;
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {

                var errors = context.ModelState.Values.SelectMany(v => v.Errors);          
         
                context.Result = new JsonResult(errors.Select(a => a.ErrorMessage).Aggregate((i, next) => $"{i},{next}"));
            }
        }
    }
}
