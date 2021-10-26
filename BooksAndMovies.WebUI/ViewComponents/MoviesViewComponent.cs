using BooksAndMovies.WebUI.Models.TMDB;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAndMovies.WebUI.ViewComponents
{
    public class MoviesViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new TMDBModel();
            string movieClientUrl = $"{model.WebsiteRootUrl}movie/top_rated?api_key={model.APIKey}";
            var movies = await model.GetMoviesFromTMDBAsync(url: movieClientUrl);
            return View(movies);

        }
    }
}
