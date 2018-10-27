using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HangfireApp.Models;
using Hangfire;

namespace HangfireApp.Controllers
{
    public class HomeController : Controller
    {
      
       
        public IActionResult Index()
        {
           
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
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

        public IActionResult AddJob()
        {
            var now = DateTime.Now;
            //后台执行一个任务
            BackgroundJob.Enqueue(() => Console.WriteLine($"Add job {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff")}"));
            //后台添加一个计划任务
            BackgroundJob.Schedule(() => Console.WriteLine($"30 秒后开始执行, Job Excete At {DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff")}"), TimeSpan.FromSeconds(30));
            return Ok("添加任务成功");
        }
    }
}
