using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Authorize_Policy_Provider.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Authorize_Policy_Provider.Authorize;

namespace Authorize_Policy_Provider.Controllers
{
    public class HomeController : Controller        
    {

        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Authorize]
        public IActionResult Contact()
        {           
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [PermissionAuthorize("Users")]
        public IActionResult Users()
        {
            return Ok("User List ,Policy Users ");
        }

        [PermissionAuthorize("User_Create")]
        public IActionResult UserCreate()
        {
            return Ok("User List ,Policy User_Create ");
        }

        [PermissionAuthorize("User_Edit")]
        public IActionResult UserEdit()
        {
            return Ok("User List ,Policy User_Edit ");
        }

        [PermissionAuthorize("User_Delete")]
        public IActionResult UserDelete()
        {
            return Ok("User List ,Policy User_Delete ");
        }

































        public IActionResult Privacy()
        {
            return View();
        }
    

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
