using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreLearn.Models
{
    public class SellDetail
    {

        public int Id { get; set; }

        public string Name { get; set; }
      
        public int? BuyerId { get; set; }

        public User Buyer { get; set; }

        public int? SellerId { get; set; }
        public  User Seller { get; set; }

    }
}
