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
using System.Text;
using System.Threading.Tasks;

namespace BooksAndMovies.WebUI.Controllers
{
    public class TVShowController : Controller
    {
        private readonly ITVShowService _tvShowService;
        private readonly IMapper _mapper;

        public TVShowController(ITVShowService tvShowService, IMapper mapper)
        {
            _tvShowService = tvShowService;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            string clientUrl = "https://api.themoviedb.org/3/tv/top_rated?api_key=ebd943da4f3d062ae4451758267b1ca9&language=en-US&page=1";
            var tvShows = await new TMDBModel().GetTVShowsFromTMDB(url: clientUrl);
            return View(tvShows);
        }

        public async Task<IActionResult> GetWishList()
        {
            var tvShowViewModel = await CreateTVShowModel(tvShowListType: "Wishlist", databaseSavingType: 1);
            return View("TVShows", tvShowViewModel);
        }

        public async Task<IActionResult> GetWatchedList()
        {
            var tvShowViewModel = await CreateTVShowModel(tvShowListType: "Watchedlist", databaseSavingType: 2);
            return View("TVShows", tvShowViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetFavouriteTVShows()
        {
            var tvShowViewModel = await CreateTVShowModel(tvShowListType: "Favouritelist", databaseSavingType: 3);
            return View("TVShows", tvShowViewModel);
        }

        private async Task<TVShowViewModel> CreateTVShowModel(string tvShowListType, int databaseSavingType)
        {
            var tvShows = await _tvShowService.GetAllAsync(x => x.DatabaseSavingType == databaseSavingType);
            var tvShowsModel = tvShows.Select(x => _mapper.Map<TVShowModel>(x)).ToList(); 
            var tvShowViewModel = new TVShowViewModel { TVShows = tvShowsModel, TVShowListType = tvShowListType };
            return tvShowViewModel;
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchTVShow(string query)
        {
            if (!string.IsNullOrEmpty(query))
            {
                string clientUrl = "https://api.themoviedb.org/3/search/tv?api_key=ebd943da4f3d062ae4451758267b1ca9&language=en-US" + "&query=" + query;
                var tvShows = await new TMDBModel().GetTVShowsFromTMDB(url: clientUrl);
                return View("Search", tvShows);
            }

            return null;

        }

        [HttpPost]
        public IActionResult TVShowDetail(TVShowModel model)
        {
            return View("Detail", model: model);
        }

        [HttpPost]
        public async Task<IActionResult> AddTVShowToWishList(TVShowModel model)
        {
            if (ModelState.IsValid)
            {
                await SaveTVShowToDatabase(model: model, 1);
            }
            return RedirectToAction("GetWishList");

        }

        [HttpPost]
        public async Task<IActionResult> AddTVShowToWatchedList(TVShowModel model)
        {
            if (ModelState.IsValid)
            {
                await SaveTVShowToDatabase(model: model, 2);

            }
            return RedirectToAction("GetWatchedList");
        }

        [HttpPost]
        public async Task<IActionResult> AddTVShowToFavouritelist(TVShowModel model)
        {
            if (ModelState.IsValid)
            {
                await SaveTVShowToDatabase(model: model, 3);
            }
            return RedirectToAction("GetWatchedList");
        }

        private async Task SaveTVShowToDatabase(TVShowModel model, int databaseSavingType)
        {
            model.RealId = model.Id;
            model.Id = 0;
            var tvShow = _mapper.Map<TVShow>(model);
            tvShow.DatabaseSavingType = databaseSavingType;
            await _tvShowService.AddAsync(tvShow);
        }

        [HttpPost]
        public async Task<IActionResult> MoveTVShowToWatchedlist(int id)
        {
            var tvShow = await _tvShowService.GetByIdAsync(id);
            if (tvShow != null)
            {
                tvShow.DatabaseSavingType = 2;
                await _tvShowService.UpdateAsync(tvShow);
            }
            return RedirectToAction("GetWatchedList");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveTVShowFromWishlist(int id)
        {
            await RemoveTVShowFromDatabase(id);
            return RedirectToAction("GetWishList");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveTVShowFromWatchedlist(int id)
        {
            await RemoveTVShowFromDatabase(id);
            return RedirectToAction("GetWatchedList");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveTVShowFromFavouritelist(int id)
        {
            await RemoveTVShowFromDatabase(id);
            return RedirectToAction("GetFavouriteTVShows");
        }

        private async Task RemoveTVShowFromDatabase(int id)
        {
            await _tvShowService.DeleteAsync(new TVShow { Id = id });
        }

        [HttpPost]
        public IActionResult RateTVShow(TVShowModel model)
        {
            return View("Rate", model: model);
        }

        [HttpGet]
        public async Task<IActionResult> Rate()
        {
            ViewBag.Rate = await GetMyRatings();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Rate(double tvShowRateValue, int tvShowId)
        {
            string clientUrl = $"https://api.themoviedb.org/3/tv/{tvShowId}/rating?api_key=ebd943da4f3d062ae4451758267b1ca9&session_id=b29465be3cbc9870641e7c32544e064c9741b6e6";
            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(new { value = tvShowRateValue }));
            var model = new TMDBModel();
            model.PostContentToTMDB(url: clientUrl, data: data);
            ViewBag.Rate = await GetMyRatings();
            return View("Rate");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRating(int id)
        {
            string clientUrl = $"https://api.themoviedb.org/3/tv/{id}/rating?api_key=ebd943da4f3d062ae4451758267b1ca9&session_id=b29465be3cbc9870641e7c32544e064c9741b6e6";
            var model = new TMDBModel();
            model.DeleteContentFromTMDB(url: clientUrl);
            ViewBag.Rate = await GetMyRatings();
            return View("Rate");
        }

        private async Task<List<TVShowModel>> GetMyRatings()
        {
            string clientUrl = "https://api.themoviedb.org/3/account/%7Baccount_id%7D/rated/tv?api_key=ebd943da4f3d062ae4451758267b1ca9&session_id=b29465be3cbc9870641e7c32544e064c9741b6e6";
            var tvShows = await new TMDBModel().GetTVShowsFromTMDB(url: clientUrl);
            return tvShows;
        }
    }
}
