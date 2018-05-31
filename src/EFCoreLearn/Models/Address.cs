using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreLearn.Models
{

    /// <summary>
    /// 标注为拥有的实体类型
    /// </summary>
   /// [Owned]
    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
    }
}
