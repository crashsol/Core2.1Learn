using System;
using System.Collections.Generic;
using System.Text;

namespace LearnGraph.Movies.Models
{
    public class Movie:Entity
    {
        public string Name { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Company { get; set; }
        public int ActorId { get; set; }

        public MovieRating MovieRating { get; set; }
       
    }
}
