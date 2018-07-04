using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Authorize_Policy_Provider.Data
{
    public class ApplicationDataSeed
    {
        private static UserManager<IdentityUser> _userManager;
        // private static RoleManager<IdentityRole> _roleManager;
        public static async Task InitDataAsync(IServiceProvider service)
        {
            var _dbcontext = service.GetRequiredService<ApplicationDbContext>();

            //更新数据库
            _dbcontext.Database.Migrate();

            _userManager = service.GetRequiredService<UserManager<IdentityUser>>();
            //  _roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();

            //var roles = new IdentityRole[]{

            //        new IdentityRole("超级管理员"),
            //        new IdentityRole("一般管理员")
            //    };
            ////Role Data Init
            //if (!_dbcontext.Roles.Any())
            //{

            //    await _dbcontext.AddRangeAsync(roles);
            //    await _dbcontext.SaveChangesAsync();
            //    var claims = new Claim[]
            //       {
            //            new Claim("Users","用户列表"),
            //            new Claim("User_Create","添加用户"),
            //            new Claim("User_Edit","编辑用户"),
            //            new Claim("User_Delete", "删除用户")

            //};
            //    foreach (var item in claims)
            //    {
            //     await   _roleManager.AddClaimAsync(roles[0], item);
            //    }
            //    for (int i = 0; i < claims.Length - 1; i++)
            //    {
            //      await  _roleManager.AddClaimAsync(roles[1], claims[i]);
            //    }


            //}

            var claims_admin = new Claim[]
            {
                 new Claim("Users","用户列表"),
                 new Claim("User_Create","添加用户"),
                 new Claim("User_Edit","编辑用户"),
                 new Claim("User_Delete", "删除用户")

            };
            var claims_user = new Claim[]
             {
                   new Claim("Users","用户列表"),
                   new Claim("User_Create","添加用户"),
                   new Claim("User_Edit","编辑用户"),

             };

            if (!_dbcontext.Users.Any())
            {
                var admin = new IdentityUser
                {
                    UserName = "admin@qq.com",
                    Email = "admin@qq.com"                 
                };
                var result = await _userManager.CreateAsync(admin, "123qwe!@#");
                await _userManager.AddClaimsAsync(admin, claims_admin);


                var user = new IdentityUser
                {
                    UserName = "user@qq.com",
                    Email = "user@qq.com"                  
                };
                result = await _userManager.CreateAsync(user, "123qwe!@#");
                await _userManager.AddClaimsAsync(user, claims_user);



            }

        }
    }
}
