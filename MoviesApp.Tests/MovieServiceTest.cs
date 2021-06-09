using NUnit.Framework;
using Moq;
using MoviesApp.Service;
using MoviesApp.MovieModals;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MoviesApp.Tests
{
    [TestFixture]
    public class MovieServiceTest
    {
        List<Movie> movieList = new List<Movie>();
        MoviesDBContext context;
        Mock<MoviesDBContext> contextMock;
        MovieService service;
        public MovieServiceTest()
        {
            movieList.Add(new Movie { Id = 1, Location = "Pune", Language = "Hindi" });
            movieList.Add(new Movie { Id = 2, Location = "Delhi", Language = "English" });
            movieList.Add(new Movie { Id = 3, Location = "Hyderabad", Language = "Hindi" });
            DbContextOptions<MoviesDBContext> options = new DbContextOptionsBuilder<MoviesDBContext>().UseInMemoryDatabase("MoviesDb").Options;
            context = new MoviesDBContext(options);
                context.Movies.AddRange(movieList);
                context.SaveChanges();
            service = new MovieService(context);
        }

       
        [Test]
        public async Task shouldReturnMovieWithLocation()
        {
            var movie = await service.GetMoive(1);
            Assert.AreEqual(movie.Id, 1);

        }
        [Test]
        public async Task shouldReturnThreeMovies()
        {
            Assert.AreEqual((await service.GetMovies()).Count(), 3);
        }
    }
}