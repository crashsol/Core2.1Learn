using LearnGraph.Movies.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LearnGraph.Movies.Services
{
    public interface IMovieService
    {
        Task<Movie> GetByIdAsync(int id);

        Task<IEnumerable<Movie>> GetAllAsync();

        Task<Movie> CreateAsync(Movie movie);
    }
}
