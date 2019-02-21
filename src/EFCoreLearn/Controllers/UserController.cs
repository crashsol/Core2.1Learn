using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreLearn.Data;
using EFCoreLearn.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace EFCoreLearn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TestDbcontext _dbcontext;

        public UserController(TestDbcontext dbcontext)
        {
            _dbcontext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
        }

        [HttpGet]
        [Route("")]
        public IActionResult Add()
        {
            var result =  _dbcontext.SellDetails
                .Include(b => b.Buyer)
                .Include(b=>b.Seller)
                .ToList();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult DelteUser(int id)
        {
            _dbcontext.Users.Remove(new User() {Id = id});
            _dbcontext.SaveChanges();
            var result = _dbcontext.SellDetails
                .Include(b => b.Buyer)
                .Include(b => b.Seller)
                .ToList();

            return Ok(result);
        }


    }
}