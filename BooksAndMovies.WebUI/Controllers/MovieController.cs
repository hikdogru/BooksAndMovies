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

        public async Task<IActionResult> GetWishList()
        {
            var movieWishList = await _movieService.GetAllAsync(x => x.DatabaseSavingType == 1);
            var movieViewModel = new MovieViewModel { Movies = movieWishList, MovieListType = "Wishlist" };
            return View("Movies", movieViewModel);
        }

        public async Task<IActionResult> GetWatchedList()
        {
            var movieWatchedList = await _movieService.GetAllAsync(x => x.DatabaseSavingType == 2);
            var movieViewModel = new MovieViewModel { Movies = movieWatchedList, MovieListType = "Watchedlist" };
            return View("Movies", movieViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetFavouriteMovies()
        {
            var favouriteMovieList = await _movieService.GetAllAsync(x => x.DatabaseSavingType == 3);
            var movieViewModel = new MovieViewModel { Movies = favouriteMovieList, MovieListType = "Favouritelist" };
            return View("Movies", movieViewModel);
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
                var movie = _mapper.Map<Movie>(model);
                movie.DatabaseSavingType = 1;
                await _movieService.AddAsync(movie);
            }
            return RedirectToAction("GetWishList");
        }


        [HttpPost]
        public async Task<IActionResult> AddMovieToWatchedList(MovieModel model)
        {
            if (ModelState.IsValid)
            {
                var movie = _mapper.Map<Movie>(model);
                movie.DatabaseSavingType = 2;
                await _movieService.AddAsync(movie);
            }
            return RedirectToAction("GetWatchedList");
        }

        [HttpPost]
        public async Task<IActionResult> AddMovieToFavouritelist(MovieModel model)
        {
            if (ModelState.IsValid)
            {
                var movie = _mapper.Map<Movie>(model);
                movie.DatabaseSavingType = 3;
                await _movieService.AddAsync(movie);
            }
            return RedirectToAction("GetWatchedList");
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
