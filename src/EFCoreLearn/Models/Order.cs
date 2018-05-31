using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreLearn.Models
{
    
    public class Order
    {

        public int Id { get; set; }

        public Address OrderAddress { get; set; }
    }
}
