using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreLearn.Data;
using EFCoreLearn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace EFCoreLearn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        

        public readonly TestDbcontext _dbContext;

        public ValuesController(TestDbcontext dbContext)
        {
            _dbContext = dbContext;           
        }
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<string>> GetAsync()
        {
            for (int i = 0; i < 1000; i++)
            {
                _dbContext.Orders.AddRange(new Order { ddd = 1, Name = "GGGGGG", Test1 = "1231231", Test2 = "11", Address = new Address { City = "DY", Street = "文庙" } });
            }
            await _dbContext.SaveChangesAsync(true);
            return Ok("ok");
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(int id)
        {
            var beef = new DbFirstModels.EFCoreLearnContext();
            return Ok(await beef.Blog.Include(b => b.Posts).ToListAsync());
        }

        [HttpGet("search/{name}")]
        public IActionResult Search(string name)
        {
            return Ok(_dbContext.Orders.AsNoTracking().Where(b => EF.Functions.Like(b.Name, $"[{name}]%")).ToList());

        }

        [Route("blogsconut")]
        [HttpGet]
        public async Task<ActionResult<List<BlogPostsCount>>> GetBlogsCountAsync()
        {
            
           return await _dbContext.BlogPostsCounts.ToListAsync();
        }


        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
