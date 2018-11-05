using AppGraph.Services;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppGraph.Middlewares
{
    public class PersonQuery : ObjectGraphType
    {
        public PersonQuery(IPersonService persionService)
        {
            //定义根据用户ID进行查询
            Field<PersonType>("person",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context =>
                {
                    var id = context.GetArgument<int>("id");
                    return persionService.GetById(id);
                });

            //定义查询全部用户
            Field<ListGraphType<PersonType>>("persons", resolve: context =>
            {
                return persionService.GetAll();

            });

        }
    }
}
