using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Movies.Controllers
{
    public class MoviesController : ApiController
    {
        public static List<string> movieTitles = new List<string>() { "value1", "value2", "value3" };

        private static List<Movie> movies = new List<Movie>() {
            new Movie { Id=1, Name="Star Wars", Director="George Lucas"},
            new Movie { Id=2, Name="Jurassic Park", Director="Steven Spielberg"},
            new Movie { Id=3, Name="Batman Begins", Director="Christopher Nolan"},

        };

        private static int movieIdCounter = 4;

        // GET api/movies
        /// <summary>
        /// Gets the list of movies stored in the server
        /// </summary>
        public IEnumerable<Movie> Get()
        {
            return movies;
        }

        // GET api/movies/5
        /// <summary>
        /// Gets a single movie that matches the id parameter
        /// </summary>
        /// <param name="id">The id of the movie you want to retrieve</param>
        public IHttpActionResult Get(int id)
        {
            var movie = movies.SingleOrDefault(x => x.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(movie);
        }

        // POST api/movies
        /// <summary>
        /// Post a new movie to the server
        /// </summary>
        public IHttpActionResult Post([FromBody]Movie movie)
        {
            movie.Id = movieIdCounter;
            movies.Add(movie);
            movieIdCounter++;

            var something = Url.Route("DefaultApi", new { @id = movie.Id, @controller = "movies" });

            return Created<Movie>(something, movie);
        }
    }

    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Director { get; set; }
    }
}
