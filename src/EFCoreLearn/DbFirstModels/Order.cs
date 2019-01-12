using System;
using System.Collections.Generic;

namespace EFCoreLearn.DbFirstModels
{
    public partial class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Test1 { get; set; }
        public string Test2 { get; set; }
        public decimal Ddd { get; set; }
        public Guid TestGuid { get; set; }

        public Address Address { get; set; }
    }
}
