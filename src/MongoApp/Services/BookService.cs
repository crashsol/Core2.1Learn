using MongoApp.Data;
using MongoApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoApp.Services
{
    public class BookService
    {
        public MongoDbContext _dbContext;
        public BookService(MongoDbContext mongoDbContext)
        {
            _dbContext = mongoDbContext;
        }

        public async Task<List<Book>> GetBooksAsync()
        {
            return await (await _dbContext.Books.FindAsync(book => true)).ToListAsync();
           
        }

        public Book Get(string id)
        {
            var docId = new ObjectId(id);
            return _dbContext.Books.Find(b => b.Id == docId).FirstOrDefault();
        }

        public Book Create(Book book)
        {
            _dbContext.Books.InsertOne(book);
            return book;
        }

        public void Update(string id,Book bookin)
        {

            var objId = new ObjectId(id);
            bookin.Id = objId;
            _dbContext.Books.ReplaceOne(b=>b.Id== objId, bookin);
        }

        public void Remove(Book book)
        {
            _dbContext.Books.DeleteOne(b => b.Id == book.Id);
        }

        public void Remove(string id)
        {
            _dbContext.Books.DeleteOne(b => b.Id == new ObjectId(id));
        }
    }
}
