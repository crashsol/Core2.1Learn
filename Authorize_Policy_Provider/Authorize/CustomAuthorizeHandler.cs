using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Authorize_Policy_Provider.Authorize
{
    /// <summary>
    /// 自定义资源验证
    /// </summary>
    public class CustomAuthorizeHandler : IAuthorizationHandler
    {
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            throw new NotImplementedException();
        }
    }
}
