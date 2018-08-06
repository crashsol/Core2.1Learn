﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreLearn.Models
{
    
    public class Order
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Test1 { get; set; }

        public string Test2 { get; set; }

        public Address Address { get; set; } 
    }
}
