using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using LearnGraph.Movies.Models;
using System.Linq;

namespace LearnGraph.Movies.Services
{
    public class MovieService : IMovieService
    {

        public readonly IList<Movie> _movies;
        public MovieService()
        {
            _movies = new List<Movie>
            {
                 new Movie{
                    Id=1,
                    ActorId=1,
                    Company ="Test1",
                   MovieRating=MovieRating.G,
                   Name = "Tset1",
                        ReleaseDate =DateTime.Now.AddYears(-1)
                },
                 new Movie{
                    Id=2,
                    ActorId=2,
                     Company ="Test2",
                      MovieRating=MovieRating.Unrated,
                       Name = "Tset2",
                        ReleaseDate =DateTime.Now.AddYears(-2)
                },
                   new Movie{
                    Id=3,
                    ActorId=3,
                     Company ="Test3",
                      MovieRating=MovieRating.NC17,
                       Name = "Tset3",
                        ReleaseDate =DateTime.Now.AddYears(-3)
                },
                     new Movie{
                    Id=4,
                    ActorId=5,
                     Company ="Test4",
                      MovieRating=MovieRating.PG,
                       Name = "Tset4",
                        ReleaseDate =DateTime.Now.AddYears(-4)
                },
                       new Movie{
                    Id=5,
                    ActorId=5,
                     Company ="Test5",
                      MovieRating=MovieRating.G,
                       Name = "Tset5",
                        ReleaseDate =DateTime.Now.AddYears(-5)
                }

            };
        }
        public Task<Movie> CreateAsync(Movie movie) 
        {
            _movies.Add(movie);
            return Task.FromResult(movie);
        }

        public Task<IEnumerable<Movie>> GetAllAsync()
        {
           
            return Task.FromResult(_movies.AsEnumerable());
        }

        public Task<Movie> GetByIdAsync(int id)
        {
            var model = _movies.FirstOrDefault(b => b.Id == id);
            if(model ==null)
            {
                throw new ArgumentException($"Movie Id {id} can't find");
            }else
            {

            }
            return Task.FromResult(model);         
        }
    }
}
