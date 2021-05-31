using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesApp.MovieModals;
using MoviesApp.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesApp.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        //api/movies
        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Movie>>> Get()
        {
            var movies = await _movieService.GetMovies();
            return Ok(movies);
        }
        //api/movies/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> Get(int id)
        {
            if (id<=0)
            {
                return NotFound();
            }
            return Ok(await _movieService.GetMoive(id));
        }
    }
}
