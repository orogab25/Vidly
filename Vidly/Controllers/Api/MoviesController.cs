using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: /api/movies
        public IHttpActionResult GetMovies()
        {
            IEnumerable<MovieDto> movieDtos = _context.Movies.Include(c => c.Genre).ToList().Select(Mapper.Map<Movie, MovieDto>);

            return Ok(movieDtos);
        }

        // GET: /api/movies/1
        public IHttpActionResult GetMovie(int id)
        {
            Movie Movie = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (Movie == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Movie, MovieDto>(Movie));
        }

        // POST: /api/movies
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto MovieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Movie Movie = Mapper.Map<MovieDto, Movie>(MovieDto);

            _context.Movies.Add(Movie);
            _context.SaveChanges();

            MovieDto.Id = Movie.Id;

            return Created(new Uri(Request.RequestUri + "/" + Movie.Id), MovieDto);
        }

        // PUT: /api/movies/1
        [HttpPut]
        public void UpdateMovie(int id, MovieDto MovieDto)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            Movie MovieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (MovieInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Mapper.Map<MovieDto, Movie>(MovieDto, MovieInDb);

            _context.SaveChanges();
        }

        // DELETE: /api/movies/1
        [HttpDelete]
        public void DeleteMovie(int id)
        {
            Movie MovieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);

            if (MovieInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            _context.Movies.Remove(MovieInDb);
            _context.SaveChanges();
        }
    }
}
