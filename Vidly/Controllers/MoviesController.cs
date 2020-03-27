using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        //GET: movies (Navigate to movies)
/*        public ActionResult Index(int? pageIndex, string sortBy) //used nullable int parameter
        {
            if (!pageIndex.HasValue)
            {
                pageIndex = 1;
            }

            if (String.IsNullOrWhiteSpace(sortBy))
            {
                sortBy = "Name";
            }
            return Content(String.Format("pageIndex={0}&sortBy={1}", pageIndex, sortBy));
        }*/
        public ActionResult Index()
        {
            List<Movie> movies = new List<Movie>()
            {
                new Movie() { Id = 1, Name = "Shrek" },
                new Movie() { Id = 2, Name = "Wall-e" }
            };

            MoviesViewModel moviesViewModel = new MoviesViewModel()
            {
                /*Movies = movies*/
                Movies = null
            };

            return View(moviesViewModel);
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