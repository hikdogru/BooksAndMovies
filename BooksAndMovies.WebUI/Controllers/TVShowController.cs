using AutoMapper;
using BooksAndMovies.Business.Abstract;
using BooksAndMovies.Entity;
using BooksAndMovies.WebUI.Models;
using BooksAndMovies.WebUI.Models.TMDB;
using BooksAndMovies.WebUI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndMovies.WebUI.Controllers
{
    public class TVShowController : Controller
    {
        #region fields
        private readonly ITVShowService _tvShowService;
        private readonly IUserTVShowService _userTVShowService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        #endregion fields

        #region ctor
        public TVShowController(ITVShowService tvShowService, IMapper mapper, IUserTVShowService userTVShowService, IUserService userService)
        {
            _tvShowService = tvShowService;
            _mapper = mapper;
            _userTVShowService = userTVShowService;
            _userService = userService;
        }
        #endregion ctor

        #region methods
        public async Task<IActionResult> Index()
        {
            var model = new TMDBModel();
            string clientUrl = $"{model.WebsiteRootUrl}tv/top_rated?api_key={model.APIKey}";
            var tvShows = await model.GetTVShowsFromTMDB(url: clientUrl);
            return View(tvShows);
        }

        public async Task<IActionResult> GetWishList()
        {
            var tvShowViewModel = await CreateTVShowModel(tvShowListType: "Wishlist", databaseSavingType: 1);
            return PartialView("_TVShowListPartial", tvShowViewModel);
        }

        public async Task<IActionResult> GetWatchedList()
        {
            var tvShowViewModel = await CreateTVShowModel(tvShowListType: "Watchedlist", databaseSavingType: 2);
            return PartialView("_TVShowListPartial", tvShowViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetFavouriteTVShows()
        {
            var tvShowViewModel = await CreateTVShowModel(tvShowListType: "Favouritelist", databaseSavingType: 3);
            return PartialView("_TVShowListPartial", tvShowViewModel);
        }

        private async Task<TVShowViewModel> CreateTVShowModel(string tvShowListType, int databaseSavingType)
        {
            var user = UserLoggedIn();
            if (user != null)
            {
                var userTVShows = await _userTVShowService.GetAllAsync(x => x.UserId == user.Id && x.DatabaseSavingType == databaseSavingType);
                var tvShows = _tvShowService.GetAll().Where(x => userTVShows.Any(y => y.TVShowId == x.Id));
                var tvShowsModel = tvShows.Select(x => _mapper.Map<TVShowModel>(x)).ToList();
                var tvShowViewModel = new TVShowViewModel { TVShows = tvShowsModel, TVShowListType = tvShowListType };
                return tvShowViewModel;
            }
            return null;
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
                var model = new TMDBModel();
                string clientUrl = $"{model.WebsiteRootUrl}search/tv?api_key={model.APIKey}" + "&query=" + query;
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
            var email = HttpContext.Session.GetString("email");
            if (email != null)
            {
                var user = await _userService.GetAllAsync(x => x.Email == email);
                var tvShowInDatabase = await _tvShowService.GetAllAsync(x => x.RealId == model.RealId);
                _userTVShowService.Add(new UserTVShow { TVShowId = tvShowInDatabase[0].Id, UserId = user[0].Id, DatabaseSavingType = databaseSavingType });
            }
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
        public async Task<IActionResult> Rate(double tvShowRateValue, int id)
        {
            var user = UserLoggedIn();
            if (user != null)
            {
                var userTVShow = _userTVShowService.GetAll(x => x.TVShowId == id && x.DatabaseSavingType == 2 && x.UserId == user.Id).SingleOrDefault();
                userTVShow.Rating = tvShowRateValue;
                await _userTVShowService.UpdateAsync(entity: userTVShow);
                ViewBag.Rate = await GetMyRatings();
                return View("Rate");
            }
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRating(int id)
        {
            var user = UserLoggedIn();
            if (user != null)
            {
                var userTVShows = await _userTVShowService.GetAllAsync(x => x.Rating > 0 && x.UserId == user.Id && x.DatabaseSavingType == 2 && x.TVShowId == id);
                userTVShows[0].Rating = 0;
                await _userTVShowService.UpdateAsync(entity: userTVShows[0]);
                ViewBag.Rate = await GetMyRatings();
                return View("Rate");
            }

            return null;
        }

        private async Task<List<TVShowModel>> GetMyRatings()
        {
            var user = UserLoggedIn();
            if (user != null)
            {
                var userTVShows = await _userTVShowService.GetAllAsync(x => x.Rating > 0 && x.UserId == user.Id && x.DatabaseSavingType == 2);
                var tvShows = _tvShowService.GetAll().Where(x => userTVShows.Any(y => y.TVShowId == x.Id)).ToList();
                var tvShowModel = tvShows.Select(x => _mapper.Map<TVShowModel>(x)).ToList();
                for (int i = 0; i < tvShowModel.Count; i++)
                {
                    tvShowModel[i].Rating = userTVShows[i].Rating;
                }
                return tvShowModel;
            }

            return null;
        }

        private User UserLoggedIn()
        {
            var email = HttpContext.Session.GetString("email");
            if (email != null)
            {
                var user = _userService.GetAll(x => x.Email == email).Single();
                return user;
            }

            return null;
        }
        #endregion methods
    }
}
