using BooksAndMovies.WebUI.Models.TMDB;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAndMovies.WebUI.ViewComponents
{
    public class TVShowsViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new TMDBModel();
            string tvShowClientUrl = $"{model.WebsiteRootUrl}tv/top_rated?api_key={model.APIKey}";
            var tvShows = await model.GetTVShowsFromTMDBAsync(url: tvShowClientUrl);
            return View(tvShows);
        }
    }
}
