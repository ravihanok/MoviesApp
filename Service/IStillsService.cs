using MoviesApp.MovieModals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApp.Service
{
    public interface IStillsService
    {
        public Task<IEnumerable<Still>> GetStills();
    }
}
