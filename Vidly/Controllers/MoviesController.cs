using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Index()
        {
            List<Movie> movies = _context.Movies.Include(m => m.Genre).ToList();

            return View(movies);
        }

        public ActionResult Details(int id)
        {
            Movie movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(m => m.Id == id); //used lambda expression

            if (movie == null)
            {
                return HttpNotFound();
            }

            return View(movie);
        }

        //GET: movies/random
        public ActionResult Random()
        {
            var movie = new Movie() {Name="Shrek"}; //used object initializer

            var customers = new List<Customer>()
            {
                new Customer { Name="Customer 1" } ,
                new Customer { Name="Customer 2" }
            };

            var viewModel = new RandomMovieViewModel()
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);
        }

        // GET: movies/edit/id
        public ActionResult Edit(int id)
        {
            return Content("id="+id);
        }

        //GET: movies/released/year/month (Movies by release date)
        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year+"/"+month);
        }
    }
}