using BooksAndMovies.WebUI.Models.TMDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAndMovies.WebUI.ViewComponents
{
    public class MoviesViewComponent : ViewComponent
    {
        private readonly IConfiguration _configuration;

        public MoviesViewComponent(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new TMDBModel(configuration: _configuration);
            string movieClientUrl = $"{model.WebsiteRootUrl}movie/top_rated?api_key={model.APIKey}";
            var movies = await model.GetMoviesFromTMDBAsync(url: movieClientUrl);
            return View(movies);

        }
    }
}
