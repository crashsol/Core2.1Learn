using DYBus.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DYBus.Data
{
   public class BusDbContent:DbContext 
    {

        public BusDbContent(DbContextOptions<BusDbContent> option) : base(option) { }

        public DbSet<BusRunTimeInfo> BusRunTimeInfos { get; set; }
    }   
}
