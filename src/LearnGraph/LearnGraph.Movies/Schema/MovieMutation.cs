using GraphQL.Types;
using LearnGraph.Movies.Models;
using LearnGraph.Movies.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using LearnGraph.Movies.Models.Dtos;

namespace LearnGraph.Movies.Schema
{
    /// <summary>
    /// Moive Mutation
    /// </summary>
    public  class MovieMutation:ObjectGraphType
    {
        public MovieMutation(IMovieService  movieService)
        {
            Name = "Mutation";
            Description = "Movie Create Mutation";
            FieldAsync<MovieType>("createMovie", 
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<MovieInputType>> { Name="movie" }),
                resolve: async context => {
                    //获取输入参数
                    var input = context.GetArgument<MovieInput>("movie");
                    var  all = await movieService.GetAllAsync();
                    var maxId = all.Max(b => b.Id) +1;
                    //Dto to Entity
                    var movie = new Movie
                    {
                        Id = maxId,
                        Name = input.Name,
                        ActorId = input.ActorId,
                        MovieRating = input.MovieRating,
                        ReleaseDate = input.ReleaseDate,
                        Company = input.Company
                    };

                    var result = await movieService.CreateAsync(movie);
                    return result;

                });
        }
    }
}
