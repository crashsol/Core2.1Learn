﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoApp.Models;
using MongoApp.Services;

namespace MongoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> GetAsync()
        {
            return await _bookService.GetBooksAsync();
        }

        [HttpGet("{id:length(24)}", Name = "GetBook")]
        public ApiReponse<Book> Get(string id)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return new ApiReponse<Book>
                {
                    Status ="error",
                    Message ="数据不存在",
                    Data=null                    
                    
                };
            }
            
            return  new ApiReponse<Book>
            {
                Status ="ok",
                Message="操作成功",
                Data = book
            };
        }

        [HttpPost]
        public ActionResult<Book> Create(Book book)
        {
            _bookService.Create(book);

            return CreatedAtRoute("GetBook", new { id = book.Id.ToString() }, book);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Book bookIn)
        {
            var book = _bookService.Get(id);

            if (book == null)
            {
                return NotFound();
            }

            _bookService.Update(id, bookIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var book = _bookService.Get(id);
            if (book == null)
            {
                return NotFound();
            }
            _bookService.Remove(book);
            return NoContent();
        }
    }
}