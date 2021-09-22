using BooksAndMovies.WebUI.Models;
using BooksAndMovies.WebUI.Models.TMDB;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAndMovies.WebUI.Controllers
{
    public class MovieController : Controller
    {
        public async Task<IActionResult> Index()
        {
            string clientUrl = "https://api.themoviedb.org/3/movie/popular?api_key=ebd943da4f3d062ae4451758267b1ca9&language=en-US";
            var movies = await new TMDBModel().GetMoviesFromTMDB(url: clientUrl);
            return View(movies);
        }

        [HttpPost]
        public async Task<IActionResult> SearchMovie(string query)
        {
            if (!string.IsNullOrEmpty(query))
            {
                string clientUrl = "https://api.themoviedb.org/3/search/movie?api_key=ebd943da4f3d062ae4451758267b1ca9&language=en-US" + "&query=" + query;
                var movies = await new TMDBModel().GetMoviesFromTMDB(url: clientUrl);
                return View("Search", movies);
            }

            return null;

        }
    }
}
