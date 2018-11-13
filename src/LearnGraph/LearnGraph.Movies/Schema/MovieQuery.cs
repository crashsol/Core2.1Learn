using GraphQL.Types;
using LearnGraph.Movies.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearnGraph.Movies.Schema
{
    /// <summary>
    /// 查询根类
    /// </summary>
    public class MovieQuery:ObjectGraphType
    {

        public MovieQuery(IMovieService movieService)
        {
            Name = "query";
            Description = "query ";

            //查询所有电影
            Field<ListGraphType<MovieType>>("movies", resolve: context => movieService.GetAllAsync());
        }
    }
}
