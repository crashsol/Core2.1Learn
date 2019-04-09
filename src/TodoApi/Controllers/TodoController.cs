using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Data;
using TodoApi.Models;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : Controller
    {
        private readonly TodoDbContext _dbContext;

        public TodoController(TodoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [ProducesResponseType(200, Type = typeof(List<TodoItem>))]
        [HttpGet]
        public async Task<IEnumerable<TodoItem>> GetAsync()
        {
            return await _dbContext.TodoItems.ToListAsync();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(TodoItem))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<TodoItem>> GetAsync(long id)
        {
            var entity = await _dbContext.TodoItems.SingleOrDefaultAsync(b => b.Id == id);
            if (entity == null)
            {
                return NotFound();
            }
            return entity;
        }

        // POST api/<controller>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> PostAsync([FromBody]TodoItem item)
        {          
            await _dbContext.TodoItems.AddAsync(item);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }


        // DELETE api/<controller>/5
        [HttpPost("del/{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var entity = await _dbContext.TodoItems.SingleOrDefaultAsync(b => b.Id == id);
            if (entity == null)
                return NotFound();

            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return Ok();
        }

       
    }
}
