using Microsoft.EntityFrameworkCore;
using MoviesApp.MovieModals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApp.Service
{
    public class StillsService : IStillsService
    {
        private readonly MoviesDBContext _context;
        public StillsService(MoviesDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Still>> GetStills()
        {
            return await _context.Stills.ToListAsync();
        }
    }
}
