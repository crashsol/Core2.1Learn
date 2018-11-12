using GraphQL.Types;
using LearnGraph.Movies.Models;
using LearnGraph.Movies.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearnGraph.Movies.Schema
{
    /// <summary>
    /// 定义Moive Graph类型
    /// </summary>
    public class MovieType : ObjectGraphType<Movie>
    {

        public MovieType(IActorService actorService)
        {
            //类型名称
            Name = "Movie";
            //类型描述
            Description = "Movie Type";

            //对外暴露的字段
            Field(x => x.Id);
            Field(x => x.Name);
            Field(x => x.Company);
            Field(x => x.ActorId);
            Field(x => x.ReleaseDate);

            //加载导航元素
            Field<ActorType>("actor", resolve: context =>  actorService.GetByIdAsync(context.Source.ActorId));

            //加载枚举变量 根据MovieRating的类型，选择 movieRating
            Field<ListGraphType<MovieRatingType>>("movieRating", resolve: context => context.Source.MovieRating);



        }
    }
}
