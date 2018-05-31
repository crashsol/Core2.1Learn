using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreLearn.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreLearn.Data.EntityConfigurations
{
    /// <summary>
    /// Order配置
    /// </summary>
    public class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            //配置Address为 拥有的实体类型 Address 的属性将出现在 Order 表中
            //builder.OwnsOne(b => b.Address);
            //如果 Order 中的Address 为private属性，则可以用以下的方法配置
            //builder.OwnsOne(typeof(Address), "Address");


            //配置初始化种子数据
            builder.HasData(new Order { Id = 1, OrderAddress = new Address { City = "上海", Street = "外滩" } },
                            new Order { Id = 2, OrderAddress = new Address { City = "成都", Street = "春熙路" } });
        }
    }
}
