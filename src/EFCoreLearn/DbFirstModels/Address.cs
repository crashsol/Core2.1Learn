using System;
using System.Collections.Generic;

namespace EFCoreLearn.DbFirstModels
{
    public partial class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public int OrderId { get; set; }

        public Order Order { get; set; }
    }
}
