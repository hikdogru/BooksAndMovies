using AutoMapper;
using BooksAndMovies.Business.Abstract;
using BooksAndMovies.Entity;
using BooksAndMovies.WebUI.Models;
using BooksAndMovies.WebUI.Models.Client;
using BooksAndMovies.WebUI.Models.TMDB;
using BooksAndMovies.WebUI.ViewModels;
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
            return View();
        }

        public async Task<IActionResult> GetWishList()
        {
            var tvShowWishList = await _tVShowService.GetAllAsync(x => x.DatabaseSavingType == 1);
            var tvShowViewModel = new TVShowViewModel { TVShows = tvShowWishList, TVShowListType = "Wishlist" };
            return View("TVShows", tvShowViewModel);
        }

        public async Task<IActionResult> GetWatchedList()
        {
            var tvShowWatchedList = await _tVShowService.GetAllAsync(x => x.DatabaseSavingType == 2);
            var tvShowViewModel = new TVShowViewModel { TVShows = tvShowWatchedList, TVShowListType = "Watchedlist" };
            return View("TVShows", tvShowViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SearchTVShow(string query)
        {
            if (!string.IsNullOrEmpty(query))
            {
                string clientUrl = "https://api.themoviedb.org/3/search/tv?api_key=ebd943da4f3d062ae4451758267b1ca9&language=en-US" + "&query=" + query;
                var tvShows = await new TMDBModel().GetTVShowsFromTMDB(url: clientUrl);
                return View("TVShowSearch", tvShows);
            }

            return null;

        }

        [HttpPost]
        public async Task<IActionResult> AddTVShowToWishList(TVShowModel model)
        {
            if (ModelState.IsValid)
            {
                var tvShow = _mapper.Map<TVShow>(model);
                tvShow.DatabaseSavingType = 1;
                await _tVShowService.AddAsync(tvShow);
            }
            return RedirectToAction("GetWishList");

        }

        [HttpPost]
        public async Task<IActionResult> AddTVShowToWatchedList(TVShowModel model)
        {
            if (ModelState.IsValid)
            {
                var tvShow = _mapper.Map<TVShow>(model);
                tvShow.DatabaseSavingType = 2;
                await _tVShowService.AddAsync(tvShow);
            }
            return RedirectToAction("GetWatchedList");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveTVShowFromWishlist(int id)
        {
            await _tVShowService.DeleteAsync(new TVShow { Id = id });
            return RedirectToAction("GetWishList");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveTVShowFromWatchedlist(int id)
        {
            await _tVShowService.DeleteAsync(new TVShow { Id = id });
            return RedirectToAction("GetWatchedList");
        }
    }
}
