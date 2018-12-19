using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using MongoApp.Models;

namespace MongoApp.Data
{
    public class MongoDbContext
    {
        private IMongoDatabase _database;
        private AppSetting _appSetting;
        public MongoDbContext(IOptionsSnapshot<AppSetting> options)
        {
            
            if (string.IsNullOrEmpty(options.Value?.DbConnection))
                throw new ArgumentNullException("Mongo Connection String is Empty");
            _appSetting = options.Value;
            var client = new MongoClient(_appSetting.DbConnection);
            if(client!=null)
            {
                _database = client.GetDatabase(_appSetting.DataBaseName);
            }
        }

        public IMongoCollection<Book> Books
        {
            get
            {
                return _database.GetCollection<Book>(nameof(Book));
            }
        }
    }
}
