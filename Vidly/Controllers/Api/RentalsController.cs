using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class RentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public RentalsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult New(RentalDto newRental)
        {
            Customer customerInDb = _context.Customers.SingleOrDefault(c=>c.Id==newRental.CustomerId);
            IEnumerable<Movie> moviesInDb = _context.Movies.Where(m => newRental.MovieIds.Contains(m.Id)).ToList();

            foreach (Movie movie in moviesInDb)
            {
                if (movie.Available == 0)
                {
                    return BadRequest("Movie is not available.");
                }

                Rental rental = new Rental
                {
                    Customer = customerInDb,
                    Movie = movie,
                    DateRented = DateTime.Now
                };
                _context.Rentals.Add(rental);
                movie.Available--;
            }
            _context.SaveChanges();

            return Ok();
        }
    }
}
