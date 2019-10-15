using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCexample.Models;
using MVCexample.ViewModel;
using System.Data.Entity;

namespace MVCexample.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies
        private ApplicationDbContext dbContext = null;
        public MoviesController()
        {
            dbContext = new ApplicationDbContext();
            
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbContext.Dispose();
            }
        }
        public ActionResult Index()
        {
            var movies = dbContext.Movies.Include(a=>a.Genre).ToList();
            return View( movies);

        }
        //public ActionResult Display()
        //{
        //    Movie movie = new Movie();
        //    movie.ID = 1;
        //    movie.MovieName = "Kappaan";
        //    return View(movie);
        //}
        //public ActionResult Display()
        //{
        //    CustomerMovieViewModel vm = new CustomerMovieViewModel();
        //    vm.Movies = new Movie { ID = 1, MovieName = "Kappaan" };
        //    vm.Customers = new List<Customer>
        //    {
        //        new Customer{ID=1,CustomerName="Viji"},
        //        new Customer{ID=2,CustomerName="Kavitha"},
        //        new Customer{ID=3,CustomerName="Kani"}
        //    };


        //    return View(vm);
        //}
        //public ActionResult DisplayCustomer()
        //{
        //    CustomerMovieViewModel vm1 = new CustomerMovieViewModel();
        //    vm1.Customers1 = new Customer { CustomerName = "Vijayalakshmi" };
        //    vm1.Movies1 = new List<Movie>
        //    {
        //        new Movie{ID=1,MovieName="ABC"},
        //        new Movie{ID=2,MovieName="XYZ"}
        //    };

        //    return View(vm1);
        //}
        public List<Movie> GetMovies()
        {
            List<Movie> movies = new List<Movie>
            {
               new Movie{ID=1,MovieName="Spider Man",ReleaseDate=Convert.ToDateTime("17/07/2000"),DateAdded=Convert.ToDateTime("11/07/2004")},
               new Movie{ID=2,MovieName="Bat Man",ReleaseDate=Convert.ToDateTime("17/11/1990"),DateAdded=Convert.ToDateTime("11/05/2011")},
               new Movie{ID=3,MovieName="Avengers",ReleaseDate=Convert.ToDateTime("24/07/2010"),DateAdded=Convert.ToDateTime("22/10/1994")},
               new Movie{ID=4,MovieName="Final Destination",ReleaseDate=Convert.ToDateTime("25/07/2015"),DateAdded=Convert.ToDateTime("23/10/2008")}
            };

            return movies;
        }

        public ActionResult MovieDetails(int id)
        {
            var movies = dbContext.Movies.Include(a=>a.Genre).ToList().SingleOrDefault(a => a.ID == id);
            return View(movies);
        }
        //public ActionResult Create()
        //{
        //    var movies = new Movie();
        //    return View();
        //}
        [HttpGet]
        public ActionResult Create()
        {
            var movies = new Movie();
            ViewBag.GenreId = ListGenres();
            return View(movies);
        }
        [HttpGet]
        public ActionResult EditMovie(int id)
        {
            var movies = dbContext.Movies.SingleOrDefault(c => c.ID == id);
            if (movies != null)
            {
                
                ViewBag.GenreId = ListGenres();
                return View(movies);


            }
            return HttpNotFound("ID does not exists ");
        }
        [HttpGet]
        public ActionResult DeleteMovie(int id)
        {
            var movies = dbContext.Movies.SingleOrDefault(c => c.ID == id);
            if (movies != null)
            {

                ViewBag.GenreId = ListGenres();
                return View(movies);


            }
            return HttpNotFound("ID does not exists ");
        }
        [HttpPost]
        public ActionResult DeleteMovie(Movie MovieFromView)
        {
            if (ModelState.IsValid)
            {
                var movieInDB = dbContext.Movies.FirstOrDefault(c => c.ID == MovieFromView.ID);
                dbContext.Movies.Remove(movieInDB);
                dbContext.SaveChanges();
                return RedirectToAction("Index", "Movies");

            }
            else
            {
                return HttpNotFound("Deleted");
            }
        }


       [HttpPost]
        public ActionResult EditMovie(Movie MovieFromView)
        {
            if (ModelState.IsValid)
            {
                var movieInDB = dbContext.Movies.FirstOrDefault(c => c.ID == MovieFromView.ID);
                movieInDB.MovieName = MovieFromView.MovieName;
                movieInDB.ReleaseDate = MovieFromView.ReleaseDate;
                movieInDB.DateAdded = MovieFromView.DateAdded;
                movieInDB.AvailableStock = MovieFromView.AvailableStock;
                movieInDB.GenreId = MovieFromView.GenreId;
                dbContext.SaveChanges();
                return RedirectToAction("Index", "Movies");
            }
            else
            {
               
                ViewBag.GenreId = ListGenres();
                return View(MovieFromView);
            }
        }





        [HttpPost]
        public ActionResult Create(Movie movieFromView)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.GenreId = ListGenres();
                return View(movieFromView);
            }
              dbContext.Movies.Add(movieFromView);
              dbContext.SaveChanges();
              return RedirectToAction("Index", "Movies");
        }
        [NonAction]
        public IEnumerable<SelectListItem> ListGenres()
        {
            var genre =  dbContext.Genres.AsEnumerable().Select(m=>new SelectListItem()
            { 
                             
                              
                         Text = m.Name,
                         Value = m.Id.ToString()
            }).ToList();
            genre.Insert(0, new SelectListItem { Text = "--Select--", Value = "0" ,Disabled=true,Selected=true});
            return genre;
        }

    }

}
