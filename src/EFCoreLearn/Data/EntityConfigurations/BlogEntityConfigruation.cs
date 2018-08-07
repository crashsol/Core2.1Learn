using EFCoreLearn.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreLearn.Data.EntityConfigurations
{
    public class BlogEntityConfigruation : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasData(new Blog { BlogId = 1, Name = "1", Url = "www" });

        }
    }
}
