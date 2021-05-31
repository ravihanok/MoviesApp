using MoviesApp.MovieModals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApp.Service
{
    public interface IMovieService
    {
        public Task<IEnumerable<Movie>> GetMovies();
        public Task<Movie> GetMoive(int id);
        
    }
}
