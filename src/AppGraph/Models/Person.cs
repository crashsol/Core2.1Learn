using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppGraph.Models
{
    public class Person
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public List<Person> Parents { get; set; } 


    }
}
