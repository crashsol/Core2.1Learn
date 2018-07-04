using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Authorize_Policy_Provider.Authorize
{
    /// <summary>
    /// 定义授权资源
    /// </summary>
    public class CustomRequirement:IAuthorizationRequirement
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string  Name { get; private set; }

        public CustomRequirement(string name)
        {
            Name = name;
        }
    }
}
