using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class TodoItem
    {
        public long Id { get; set; }

        [Required(ErrorMessage ="名称不能为空")]
        public string Name { get; set; }
        public bool IsComplete { get; set; }        


        public Dictionary<string,object> KeyValuePairs { get; set; }
    }
}
