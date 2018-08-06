using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;
using EFCoreLearn.Data.EntityConfigurations;
using EFCoreLearn.Models;
using EFCoreLearn.Data;
using System.Reflection;

namespace EFCoreLearn.Data
{
    public class TestDbcontext : DbContext
    {
        public TestDbcontext(DbContextOptions<TestDbcontext> options) : base(options) { }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Blog> Blogs { get; set; }


        /// <summary>
        /// 视图查询 需要使用DbQuery ,永远不会跟踪的更改上_DbContext_并因此永远不会插入、 更新或删除数据库上。
        /// </summary>
        public DbQuery<BlogPostsCount> BlogPostsCounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());



        }
    }
}
