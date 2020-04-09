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

        // GET: Movies
        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.CanManageMovies))
            {
                return View("List");
            }

            return View("ReadOnlyList");
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            List<Genre> genres = _context.Genres.ToList();

            MovieFormViewModel movieFormViewModel = new MovieFormViewModel()
            {
                Movie = new Movie(),
                Genres = genres
            };

            return View("MovieForm",movieFormViewModel);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
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
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                MovieFormViewModel movieFormViewModel = new MovieFormViewModel()
                {
                    Movie = movie,
                    Genres=_context.Genres.ToList()
                };

                return View("MovieForm",movieFormViewModel);
            }

            //New movie
            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);

            }
            //Existing movie
            else
            {
                Movie movieInDb = _context.Movies.Single(m=>m.Id==movie.Id);

                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.DateAdded = DateTime.Now;
                movieInDb.InStock = movie.InStock;
            }
            _context.SaveChanges();

            return RedirectToAction("Index","Movies");
        }
    }
}