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
        public TestDbcontext(DbContextOptions<TestDbcontext> options) : base(options) {}

        public DbSet<Order> Orders { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Blog> Blogs { get; set; }


        /// <summary>
        /// 视图查询 需要使用DbQuery ,永远不会跟踪的更改上_DbContext_并因此永远不会插入、 更新或删除数据库上。
        /// </summary>
        public DbQuery<BlogPostsCount> BlogPostsCounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            ///获取所有的实体类
            foreach (var item in modelBuilder.Model.GetEntityTypes())
            {
               if(!item.ClrType.Name.Equals("BlogPostsCount"))
                {
                    //统一设置所有表名称
                    modelBuilder.Entity(item.Name).ToTable(item.ClrType.Name);
                }
        

           


                //所有类中所有string类型的属性
                foreach (var property in item.GetProperties().Where(b=>b.ClrType == typeof(string)))
                {
                    //设置所有的string最大长度为50
                    property.SetMaxLength(80);
                }

            }

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());



        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ChangeTracker.DetectChanges();
            foreach (var item in ChangeTracker.Entries().Where(e=>e.State ==EntityState.Added))
            {
                this.AddRange(item.Entity);
            }
            //关闭追踪
            ChangeTracker.AutoDetectChangesEnabled = false;
            var result = base.SaveChanges(acceptAllChangesOnSuccess);
            //开启追踪
            ChangeTracker.AutoDetectChangesEnabled = true;
            return result;
        }
    }
}
