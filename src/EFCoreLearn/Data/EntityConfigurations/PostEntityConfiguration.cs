using EFCoreLearn.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreLearn.Data.EntityConfigurations
{
    public class PostEntityConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Post"); 
            builder.HasData(
                new Post() { PostId = 1, Content = "123", Title = "123", BlogId = 1 },
                new Post() { PostId = 2, Content = "123", Title = "123", BlogId = 1 },
                new Post() { PostId = 3, Content = "123", Title = "123", BlogId = 1 });
        }
    }
}
