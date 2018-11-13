using GraphQL.Types;
using LearnGraph.Movies.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearnGraph.Movies.Schema
{
   public   class MovieInputType:InputObjectGraphType<MovieInput>
    {
        public MovieInputType()
        {
            Name = "MovieInput";
            Description = "Create Moive Dto";
            Field(b => b.Name);
            Field(b => b.ReleaseDate);
            Field(b => b.Company);
            Field(b => b.ActorId);
            Field(b => b.MovieRating,type:typeof(MovieRatingType));          

        }
    }
}
