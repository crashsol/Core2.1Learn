using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authorize_Policy_Provider.Authorize
{
    /// <summary>
    /// 自定义授权标记
    /// </summary>
    public class PermissionAuthorizeAttribute:AuthorizeAttribute
    {
       /// <summary>
       /// 策略前缀
       /// </summary>
        private const string POLICY_PREFIX = "Permission_";

        //使用构造函数，出入PermissionName ，并使用该名称创建对应的 PolicyName
        public PermissionAuthorizeAttribute(string name) => Name = name;
        
        
        public string Name
        {
            get
            {
                return Policy.Substring(POLICY_PREFIX.Length);
            }
            set
            {
                Policy = $"{POLICY_PREFIX}{value??""}";
            }
        }


        


    }
}
