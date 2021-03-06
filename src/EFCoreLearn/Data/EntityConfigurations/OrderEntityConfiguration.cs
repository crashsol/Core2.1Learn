﻿using System;
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
       
            //配置Address为 拥有的实体类型 Address 的属性将出现在 Order 表中
            builder.OwnsOne(b => b.Address);

            builder.Property(b => b.Test2).HasMaxLength(120);
            builder.Property(b => b.ddd).HasColumnType("decimal(18,2)");

            builder.Property(b => b.TestGuid).HasDefaultValueSql("NEWID()");
           
        }
    }
}
