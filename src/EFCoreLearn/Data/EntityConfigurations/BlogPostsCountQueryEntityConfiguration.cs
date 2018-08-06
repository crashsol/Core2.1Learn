using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreLearn.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreLearn.Data.EntityConfigurations
{
    public class BlogPostsCountQueryEntityConfiguration : IQueryTypeConfiguration<BlogPostsCount>
    {
        public void Configure(QueryTypeBuilder<BlogPostsCount> builder)
        {
            //配置视图查询
            builder.ToView("View_BlogPostCounts")
                    .Property(b => b.BlogName).HasColumnName("Name");
        }
    }
}
