using AutoMapper;
using BooksAndMovies.Business.Abstract;
using BooksAndMovies.Entity;
using BooksAndMovies.WebUI.Models;
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
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;


        public MovieController(IMovieService movieService, IMapper mapper)
        {
            _movieService = movieService;
            _mapper = mapper;
        }
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
            var movies = await _movieService.GetAllAsync(x => x.DatabaseSavingType == databaseSavingType);
            var movieViewModel = new MovieViewModel { Movies = movies, MovieListType = movieListType };
            return movieViewModel;
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
            var movie = _mapper.Map<Movie>(model);
            movie.DatabaseSavingType = databaseSavingType;
            await _movieService.AddAsync(movie);
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
    }
}
