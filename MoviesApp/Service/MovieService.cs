using Microsoft.EntityFrameworkCore;
using MoviesApp.MovieModals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApp.Service
{
    public class MovieService : IMovieService
    {
        private readonly MoviesDBContext _context;
        public MovieService(MoviesDBContext context)
        {
            _context = context;
        }
        public async Task<Movie> GetMoive(int id)
        {
            return await _context.Movies.Include(i=>i.Stills).SingleOrDefaultAsync(i=>i.Id==id);
        }

        public async Task<IEnumerable<Movie>> GetMovies()
        {
            return await _context.Movies.Include(i => i.Stills).ToListAsync();
        }
    }
}
