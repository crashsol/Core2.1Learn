using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppGraph.Models;

namespace AppGraph.Services
{
    public class PersonService : IPersonService
    {
        public IEnumerable<Person> GetAll()
        {
            var michael = new Person {
                Id =1,
                Name="Michael",
                Email="Michaale@qq.com"
            };

            var carol = new Person
            {
                Id = 2,
                Name = "carol",
                Email = "carol@qq.com"
            };

            var dave = new Person
            {
                Id = 3,
                Name = "dave",
                Email = "dave@qq.com",
                Parents = new[] { carol, michael }.ToList()
            };

            var nick = new Person
            {
                Id = 4,
                Name = "nick",
                Email = "nick@qq.com",
                Parents = new[] { carol, michael }.ToList()
            };

            return new List<Person> { michael, dave, nick, carol }.ToList();



        }

        public Person GetById(int id)
        {
            return GetAll().SingleOrDefault(b => b.Id == id);
        }
    }
}
