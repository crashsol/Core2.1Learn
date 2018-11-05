using AppGraph.Services;
using GraphQL;
using GraphQL.Http;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AppGraph.Middlewares
{
    public class PersonGraphMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly IPersonService _personService;

        public PersonGraphMiddleware(RequestDelegate next,IPersonService personService)
        {
            _next = next;
            _personService = personService;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if(httpContext.Request.Path.StartsWithSegments("/graphql"))
            {
                using (var stream = new StreamReader(httpContext.Request.Body))
                {
                    var query = await stream.ReadToEndAsync();
                    if(!string.IsNullOrWhiteSpace(query))
                    {
                        var sechama = new Schema { Query = new PersonQuery(_personService) };
                        var result = await new DocumentExecuter().ExecuteAsync(options =>
                        {

                            options.Schema = sechama;
                            options.Query = query;

                        });
                       await WriteResult(httpContext, result);
                    }
                }

            }
            else
            {
                await _next(httpContext);
            }
        }


        public async Task WriteResult(HttpContext http,ExecutionResult result)
        {
            var json = new DocumentWriter(indent: true).Write(result);
            http.Response.StatusCode = 200;
            http.Response.ContentType = "application/json";
            await http.Response.WriteAsync(json);
        }
    }
}
