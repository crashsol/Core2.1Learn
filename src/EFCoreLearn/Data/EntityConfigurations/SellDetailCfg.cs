using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreLearn.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage;

namespace EFCoreLearn.Data.EntityConfigurations
{
    public class SellDetailCfg : IEntityTypeConfiguration<SellDetail>
    {
        public void Configure(EntityTypeBuilder<SellDetail> builder)
        {
            builder.HasKey(b => b.Id);

          

            builder.HasData(new SellDetail()
            {
                Id = 1,
                BuyerId = 1,
                SellerId = 2
             });

        }
    }
}
