﻿using System;
using System.Collections.Generic;

namespace EFCoreLearn.DbFirstModels
{
    public partial class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int BlogId { get; set; }
    }
}
