using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
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

        //GET: movies/released/year/month (Movies by release date)
        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year+"/"+month);
        }

        public ActionResult New()
        {
            List<Genre> genres = _context.Genres.ToList();

            MovieFormViewModel movieFormViewModel = new MovieFormViewModel()
            {
                Genres = genres
            };

            return View("MovieForm",movieFormViewModel);
        }

        public ActionResult Edit(int id)
        {
            Movie movie = _context.Movies.SingleOrDefault(m => m.Id == id);

            if(movie==null)
            {
                return HttpNotFound();
            }

            MovieFormViewModel movieFormViewModel = new MovieFormViewModel()
            {
                Movie = movie,
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm",movieFormViewModel);
        }

        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            if (movie.Id == 0)
            {
                _context.Movies.Add(movie);
            }
            else
            {
                Movie movieInDb = _context.Movies.Single(m=>m.Id==movie.Id);

                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.Genre = movie.Genre;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.InStock = movie.InStock;
                movieInDb.DateAdded = DateTime.Now;
            }
            _context.SaveChanges();

            return RedirectToAction("Index","Movies");
        }
    }
}