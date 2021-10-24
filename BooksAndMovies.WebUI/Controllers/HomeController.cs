using BooksAndMovies.WebUI.Models;
using BooksAndMovies.WebUI.Models.TMDB;
using BooksAndMovies.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace BooksAndMovies.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var model = new TMDBModel();
            string movieClientUrl = $"{model.WebsiteRootUrl}movie/top_rated?api_key={model.APIKey}";
            string tvShowClientUrl = $"{model.WebsiteRootUrl}tv/top_rated?api_key={model.APIKey}";
            var movies = await model.GetMoviesFromTMDBAsync(url: movieClientUrl);
            var tvShows = await model.GetTVShowsFromTMDBAsync(url: tvShowClientUrl);
            return View(model: new MovieTVShowViewModel { Movies = movies, TVShows = tvShows });
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
