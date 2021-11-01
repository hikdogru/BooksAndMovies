using BooksAndMovies.WebUI.Models.TMDB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAndMovies.WebUI.ViewComponents
{
    public class TVShowsViewComponent : ViewComponent
    {

        private readonly IConfiguration _configuration;

        public TVShowsViewComponent(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = new TMDBModel(configuration: _configuration);
            string tvShowClientUrl = $"{model.WebsiteRootUrl}tv/top_rated?api_key={model.APIKey}";
            var tvShows = await model.GetTVShowsFromTMDBAsync(url: tvShowClientUrl);
            return View(tvShows);
        }
    }
}
