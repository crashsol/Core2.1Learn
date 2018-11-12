using System;
using System.Collections.Generic;
using System.Text;
using GraphQL;
using GraphQL.Types;
using LearnGraph.Movies.Models;

namespace LearnGraph.Movies.Schema
{
    /// <summary>
    /// 枚举变量Type定义
    /// </summary>
    public class MovieRatingType : EnumerationGraphType
    {
        public MovieRatingType()
        {
            Name = "MovieRating";
            Description = "电影分级";
            AddValue(MovieRating.Unrated.ToString(), MovieRating.Unrated.ToString(), MovieRating.Unrated);
            AddValue(MovieRating.G.ToString(), MovieRating.G.ToString(), MovieRating.G);
            AddValue(MovieRating.NC17.ToString(), MovieRating.NC17.ToString(), MovieRating.NC17);
            AddValue(MovieRating.PG.ToString(), MovieRating.PG.ToString(), MovieRating.PG);
            AddValue(MovieRating.PG13.ToString(), MovieRating.PG13.ToString(), MovieRating.PG13);
            AddValue(MovieRating.R.ToString(), MovieRating.R.ToString(), MovieRating.R);
        }
    }
}
