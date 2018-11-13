using GraphQL;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearnGraph.Movies.Schema
{
    /// <summary>
    /// 定义Schema，
    /// 一个完整Schema包含
    /// 1、Query
    /// 2、Mutations
    /// 3、Subscriptions
    /// </summary>
    public class MovieSchema:GraphQL.Types.Schema
    {

        public MovieSchema(IDependencyResolver dependencyResolver,
            MovieQuery movieQuery)
        {
            DependencyResolver = dependencyResolver;
            Query = movieQuery;
        }
    }
}
