using System;
using System.Collections.Generic;

namespace EFCoreLearn.DbFirstModels
{
    public partial class Blog
    {
        public int BlogId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }

        public  List<Post> Posts { get; set; }
    }
}
