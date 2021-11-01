using AutoMapper;
using BooksAndMovies.Business.Abstract;
using BooksAndMovies.Entity;
using BooksAndMovies.WebUI.Models;
using BooksAndMovies.WebUI.Models.TMDB;
using BooksAndMovies.WebUI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;

        #endregion fields

        #region ctor
        public MovieController(IMovieService movieService, IMapper mapper, IUserMovieService userMovieService, IUserService userService, IConfiguration configuration)
        {
            _movieService = movieService;
            _mapper = mapper;
            _userMovieService = userMovieService;
            _userService = userService;
            _configuration = configuration;
        }
        #endregion ctor

        #region methods
        public async Task<IActionResult> Index()
        {
            var model = new TMDBModel(configuration: _configuration);
            string clientUrl = $"{model.WebsiteRootUrl}movie/popular?api_key={model.APIKey}";
            var movies = await model.GetMoviesFromTMDBAsync(url: clientUrl);
            return View(movies);
        }

        [HttpGet]
        public async Task<IActionResult> GetWishList()
        {
            var movieViewModel = await CreateMovieModel("Wishlist", 1);
            return PartialView("_MovieListPartial", movieViewModel);

        }

        [HttpGet]
        public async Task<IActionResult> GetWatchedList()
        {
            var movieViewModel = await CreateMovieModel("Watchedlist", 2);
            return PartialView("_MovieListPartial", movieViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> GetFavouriteMovies()
        {
            var movieViewModel = await CreateMovieModel("Favouritelist", 3);
            return PartialView("_MovieListPartial", movieViewModel);
        }

        private async Task<MovieViewModel> CreateMovieModel(string movieListType, int databaseSavingType)
        {
            var user = UserLoggedIn();
            if (user != null)
            {
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
                var model = new TMDBModel(configuration: _configuration);
                string clientUrl = $"{model.WebsiteRootUrl}search/movie?api_key={model.APIKey}" + "&query=" + query;
                var movies = await model.GetMoviesFromTMDBAsync(url: clientUrl);
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
            var user = UserLoggedIn();
            if (user != null)
            {
                var movieInDatabase = await _movieService.GetAllAsync(x => x.RealId == model.RealId);
                _userMovieService.Add(new UserMovie { MovieId = movieInDatabase[0].Id, UserId = user.Id, DatabaseSavingType = databaseSavingType });
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
            var user = UserLoggedIn();
            if (user != null)
            {
                var userMovie = _userMovieService.GetAll(x => x.MovieId == id && x.DatabaseSavingType == 2 && x.UserId == user.Id).SingleOrDefault();
                userMovie.Rating = movieRateValue;
                await _userMovieService.UpdateAsync(entity: userMovie);
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
                var userMovies = await _userMovieService.GetAllAsync(x => x.Rating > 0 && x.UserId == user.Id && x.DatabaseSavingType == 2 && x.MovieId == id);
                userMovies[0].Rating = 0;
                await _userMovieService.UpdateAsync(entity: userMovies[0]);
                ViewBag.Rate = await GetMyRatings();
                return View("Rate");
            }

            return null;
        }

        private async Task<List<MovieModel>> GetMyRatings()
        {
            var user = UserLoggedIn();
            if (user != null)
            {
                var userMovies = await _userMovieService.GetAllAsync(x => x.Rating > 0 && x.UserId == user.Id && x.DatabaseSavingType == 2);
                var movies = _movieService.GetAll().Where(x => userMovies.Any(y => y.MovieId == x.Id)).ToList();
                var movieModel = movies.Select(x => _mapper.Map<MovieModel>(x)).ToList();
                for (int i = 0; i < movieModel.Count; i++)
                {
                    movieModel[i].Rating = userMovies[i].Rating;
                }
                return movieModel;
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
