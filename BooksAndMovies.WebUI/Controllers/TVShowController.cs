using AutoMapper;
using BooksAndMovies.Business.Abstract;
using BooksAndMovies.Entity;
using BooksAndMovies.WebUI.Models;
using BooksAndMovies.WebUI.Models.Client;
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
    public class TVShowController : Controller
    {
        private readonly ITVShowService _tVShowService;
        private readonly IMapper _mapper;

        public TVShowController(ITVShowService tVShowService, IMapper mapper)
        {
            _tVShowService = tVShowService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            string clientUrl = "https://api.themoviedb.org/3/tv/popular?api_key=ebd943da4f3d062ae4451758267b1ca9&language=en-US";
            var tvShows = await new TMDBModel().GetTVShowsFromTMDB(url: clientUrl);
            return View(tvShows);
        }

        [HttpPost]
        public async Task<IActionResult> SearchTVShow(string query)
        {
            if (!string.IsNullOrEmpty(query))
            {
                string clientUrl = "https://api.themoviedb.org/3/search/tv?api_key=ebd943da4f3d062ae4451758267b1ca9&language=en-US" + "&query=" + query;
                var tvShows = await new TMDBModel().GetTVShowsFromTMDB(url: clientUrl);
                var tvShow = _mapper.Map<TVShowWatchList>(tvShows[0]);
                _tVShowService.Add(tvShow);
                return View("Search", tvShows);
            }

            return null;

        }

    }
}
