using AppGraph.Models;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppGraph.Middlewares
{

    /// <summary>
    /// 定义GraphType
    /// </summary>
    public class PersonType : ObjectGraphType<Person>
    {
        public PersonType()
        {
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Email);            
            Field<ListGraphType<PersonType>>("Parents");            

        }
    }
}
