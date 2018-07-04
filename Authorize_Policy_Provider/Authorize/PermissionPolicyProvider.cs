using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Authorize_Policy_Provider.Authorize
{

    /// <summary>
    /// 自定义策略提供者 
    /// </summary>
    internal class PermissionPolicyProvider : IAuthorizationPolicyProvider
    {
        const string POLICY_PREFIX = "Permission_";

        /// <summary>
        /// 默认授权策略 [Authoize]
        /// </summary>
        /// <returns></returns>
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            return Task.FromResult(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build());
        }

        //自定义创建策略
        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            
           if(policyName.StartsWith(POLICY_PREFIX,StringComparison.OrdinalIgnoreCase))
            {
                
                //PermissionAuthorizeAttribute 
                //Create new Policy
                var policy = new AuthorizationPolicyBuilder();
                //基于资源授权
                //policy.AddRequirements(new PermissionRequirement(policyName.Substring(POLICY_PREFIX.Length)));
                //基于Claims 授权
                policy.RequireClaim(policyName.Substring(POLICY_PREFIX.Length));

                return Task.FromResult(policy.Build());
            }
            return Task.FromResult<AuthorizationPolicy>(null);
        }
    }
}
