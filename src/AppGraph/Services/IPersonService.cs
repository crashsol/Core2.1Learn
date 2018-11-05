using AppGraph.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppGraph.Services
{
  public  interface IPersonService
    {
        IEnumerable<Person> GetAll();

        Person GetById(int id);
    }
}
