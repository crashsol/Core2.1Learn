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
            builder.OwnsOne(b => b.Address);
           
       
        }
    }
}
