using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;
using EFCoreLearn.Data.EntityConfigurations;
using EFCoreLearn.Models;

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

            modelBuilder.Entity<Order>(b =>
            {
                b.ToTable("Order");

                ////配置Address为Owned类型
                b.OwnsOne(a => a.OrderAddress);

                //Owned类型不能添加初始化数据
                //b.HasData(new Order
                //{
                //    Id = 1,
                //    OrderAddress = new Address { City = "123", Street = "123" }
                //});    

                //Owned 不支持EntityConfiguration
                //  modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
            });
          
            modelBuilder.Entity<Post>(option =>
            {
                option.ToTable("Post");
                option.HasData(
                    new Post() { PostId = 1, Content = "123", Title = "123", BlogId = 1 },
                    new Post() { PostId = 2, Content = "123", Title = "123", BlogId = 1 },
                    new Post() { PostId = 3, Content = "123", Title = "123", BlogId = 1 });
            });

            modelBuilder.Entity<Blog>(option =>
            {
                option.ToTable("Blog");
                option.HasData(new Blog { BlogId = 1, Name = "1", Url = "www" });

            });

            //配置视图查询
            modelBuilder.Query<BlogPostsCount>().ToView("View_BlogPostCounts")
                    .Property(b => b.BlogName).HasColumnName("Name");
           
        }
    }
}
