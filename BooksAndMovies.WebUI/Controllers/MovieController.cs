using AutoMapper;
using BooksAndMovies.Business.Abstract;
using BooksAndMovies.Entity;
using BooksAndMovies.WebUI.Models;
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
            var movieWishList = await _movieService.GetAllAsync();
            return View("WishList", movieWishList);
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
        public async Task<IActionResult> AddMovieToWishList(MovieModel model)
        {
            if (ModelState.IsValid)
            {
                var movie = _mapper.Map<MovieWatchList>(model);
                await _movieService.AddAsync(movie);
                

            }

            return RedirectToAction("GetWishList");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveMovieFromWishList(int id)
        {
            await _movieService.DeleteAsync(new MovieWatchList { Id = id });
            return RedirectToAction("GetWishList");
        }
    }
}
