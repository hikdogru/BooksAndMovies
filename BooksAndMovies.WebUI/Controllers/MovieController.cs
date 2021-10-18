﻿using AutoMapper;
using BooksAndMovies.Business.Abstract;
using BooksAndMovies.Entity;
using BooksAndMovies.WebUI.Models;
using BooksAndMovies.WebUI.Models.TMDB;
using BooksAndMovies.WebUI.ViewModels;
using Microsoft.AspNetCore.Http;
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
    public class MovieController : Controller
    {
        #region fields
        private readonly IMovieService _movieService;
        private readonly IUserMovieService _userMovieService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        #endregion fields

        #region ctor
        public MovieController(IMovieService movieService, IMapper mapper, IUserMovieService userMovieService, IUserService userService)
        {
            _movieService = movieService;
            _mapper = mapper;
            _userMovieService = userMovieService;
            _userService = userService;
        }
        #endregion ctor

        #region methods
        public async Task<IActionResult> Index()
        {
            string clientUrl = "https://api.themoviedb.org/3/movie/popular?api_key=ebd943da4f3d062ae4451758267b1ca9&language=en-US";
            var movies = await new TMDBModel().GetMoviesFromTMDB(url: clientUrl);
            return View(movies);
        }

        [HttpGet]
        public async Task<IActionResult> GetWishList()
        {
            var movieViewModel = await CreateMovieModel("Wishlist", 1);
            return View("Movies", movieViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetWatchedList()
        {
            var movieViewModel = await CreateMovieModel("Watchedlist", 2);
            return View("Movies", movieViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetFavouriteMovies()
        {
            var movieViewModel = await CreateMovieModel("Favouritelist", 3);
            return View("Movies", movieViewModel);
        }

        private async Task<MovieViewModel> CreateMovieModel(string movieListType, int databaseSavingType)
        {
            var email = HttpContext.Session.GetString("email");
            if (email != null)
            {
                var user =  _userService.GetAll(x => x.Email == email).SingleOrDefault();
                var userMovies = await _userMovieService.GetAllAsync(x => x.UserId == user.Id && x.DatabaseSavingType == databaseSavingType);
                var movies = _movieService.GetAll().Where(x => userMovies.Any(y => y.MovieId == x.Id));
                var moviesModel = movies.Select(x => _mapper.Map<MovieModel>(x)).ToList();
                var movieViewModel = new MovieViewModel { Movies = moviesModel, MovieListType = movieListType };
                return movieViewModel;
            }

            return null;
        }


        [HttpGet]
        public IActionResult Search()
        {
            return View();
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

        [HttpPost]
        public IActionResult MovieDetail(MovieModel model)
        {
            return View("Detail", model: model);
        }

        [HttpPost]
        public async Task<IActionResult> AddMovieToWishList(MovieModel model)
        {
            if (ModelState.IsValid)
            {
                await SaveMovieToDatabase(model: model, databaseSavingType: 1);
            }
            return RedirectToAction("GetWishList");
        }


        [HttpPost]
        public async Task<IActionResult> AddMovieToWatchedList(MovieModel model)
        {
            if (ModelState.IsValid)
            {
                await SaveMovieToDatabase(model: model, databaseSavingType: 2);
            }
            return RedirectToAction("GetWatchedList");
        }

        [HttpPost]
        public async Task<IActionResult> AddMovieToFavouritelist(MovieModel model)
        {
            if (ModelState.IsValid)
            {
                await SaveMovieToDatabase(model: model, databaseSavingType: 3);
            }
            return RedirectToAction("GetWatchedList");
        }

        private async Task SaveMovieToDatabase(MovieModel model, int databaseSavingType)
        {
            model.RealId = model.Id;
            model.Id = 0;
            var movie = _mapper.Map<Movie>(model);
            movie.DatabaseSavingType = databaseSavingType;
            await _movieService.AddAsync(movie);
            var email = HttpContext.Session.GetString("email");
            if (email != null)
            {
                var movieInDatabase = await _movieService.GetAllAsync(x => x.RealId == model.RealId);
                var user = await _userService.GetAllAsync(x => x.Email == email);
                _userMovieService.Add(new UserMovie { MovieId = movieInDatabase[0].Id, UserId = user[0].Id, DatabaseSavingType = databaseSavingType });
            }

        }

        [HttpPost]
        public async Task<IActionResult> MoveMovieToWatchedlist(int id)
        {
            var movie = await _movieService.GetByIdAsync(id);
            if (movie != null)
            {
                movie.DatabaseSavingType = 2;
                await _movieService.UpdateAsync(movie);
            }
            return RedirectToAction("GetWatchedList");
        }


        [HttpPost]
        public async Task<IActionResult> RemoveMovieFromWishlist(int id)
        {
            await RemoveMovieFromDatabase(id);
            return RedirectToAction("GetWishList");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveMovieFromWatchedlist(int id)
        {
            await RemoveMovieFromDatabase(id);
            return RedirectToAction("GetWatchedList");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveMovieFromFavouritelist(int id)
        {
            await RemoveMovieFromDatabase(id);
            return RedirectToAction("GetFavouriteMovies");
        }

        private async Task RemoveMovieFromDatabase(int id)
        {
            await _movieService.DeleteAsync(new Movie { Id = id });
        }

        [HttpPost]
        public IActionResult RateMovie(MovieModel model)
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
        public async Task<IActionResult> Rate(double movieRateValue, int id)
        {
            var email = HttpContext.Session.GetString("email");
            if (email != null)
            {
                var user = await _userService.GetAllAsync(x => x.Email == email);
                var userMovie = _userMovieService.GetAll(x => x.MovieId == id && x.DatabaseSavingType == 2 && x.UserId == user[0].Id).SingleOrDefault();
                userMovie.Rating = movieRateValue;
                await _userMovieService.UpdateAsync(entity: userMovie);
                //ViewBag.Rate = await GetMyRatings();
                return View("Rate");
            }
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRating(int id)
        {
            string clientUrl = $"https://api.themoviedb.org/3/movie/{id}/rating?api_key=ebd943da4f3d062ae4451758267b1ca9&session_id=b29465be3cbc9870641e7c32544e064c9741b6e6";
            var model = new TMDBModel();
            model.DeleteContentFromTMDB(url: clientUrl);
            ViewBag.Rate = await GetMyRatings();
            return View("Rate");
        }

        private async Task<List<MovieModel>> GetMyRatings()
        {
            var email = HttpContext.Session.GetString("email");
            if (email != null)
            {
                var user = await _userService.GetAllAsync(x => x.Email == email);
                var userMovies = await _userMovieService.GetAllAsync(x => x.Rating > 0 && x.UserId == user[0].Id && x.DatabaseSavingType == 2);
                var movies = _movieService.GetAll().Where(x => userMovies.Any(y => y.MovieId == x.Id)).ToList();
                var movieModel = movies.Select(x => _mapper.Map<MovieModel>(x)).ToList();
                return movieModel;
            }
            return null;
        }

        #endregion methods
    }
}
