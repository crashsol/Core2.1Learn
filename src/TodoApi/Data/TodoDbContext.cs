using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Data
{
    public class TodoDbContext:DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }

        public DbSet<TodoItem> TodoItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>().ToTable("TodoItem");

            modelBuilder.Entity<TodoItem>(opt =>
            {

                opt.Property<Dictionary<string, object>>(nameof(TodoItem.KeyValuePairs))
                     .HasConversion(
                         d => JsonConvert.SerializeObject(d, Formatting.None),
                         s => JsonConvert.DeserializeObject<Dictionary<string, object>>(s)
                     )
                     .HasColumnName(nameof(TodoItem.KeyValuePairs));
            });
        }

    }
}
