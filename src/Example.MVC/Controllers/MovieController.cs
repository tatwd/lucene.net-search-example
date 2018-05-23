using Example.MovieStore;
using Example.MVC.Models;
using LuceneSearchUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Example.MVC.Controllers
{
    public class MovieController : Controller
    {
        // GET: Movie
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(string q)
        {
            if (String.IsNullOrEmpty(q))
                return View("Index");

            ExampleMovieStoreEntities db = new ExampleMovieStoreEntities();
            var smovies = ToMovieSearchModelList(db.Movies.ToList());
            string distPath = Server.MapPath("~/App_Data/lucene-data/index01");

            SearchEngine.LoadFSDirectory(distPath);
            // SearchEngine.LoadRAMDDir();

            SearchEngine.Index<MovieSearchModel>(smovies);
            //var result = SearchEngine.Search<MovieSearchModel>("FullText", q, 10);
            var result = SearchEngine.SearchFullText<MovieSearchModel>(q, 10)
                .OrderByDescending(x => x.DoubanRating)
                .ToList(); ;

            return View("Index", result);
        }

        protected IList<MovieSearchModel> ToMovieSearchModelList(IList<Movie> movies)
        {
            IList<MovieSearchModel> smovies = new List<MovieSearchModel>();

            foreach (var movie in movies)
            {
                smovies.Add(new MovieSearchModel
                {
                    Title = movie.Title,
                    ReleaseAt = DateTime.Parse(movie.ReleaseAt.ToString()),
                    AbstractText = movie.Stroryline.Substring(0, movie.Stroryline.Length / 2) + "……",
                    DoubanRating = Decimal.Parse(movie.DoubanRating.ToString())
                });
            }

            return smovies;
        }
    }
}