using AutoMapper;
using BooksAndMovies.Business.Abstract;
using BooksAndMovies.Data;
using BooksAndMovies.Data.Concrete.Ef;
using BooksAndMovies.Entity;
using BooksAndMovies.WebUI.Models;
using BooksAndMovies.WebUI.Models.GoogleApi;
using BooksAndMovies.WebUI.ViewModels;
using Microsoft.AspNetCore.Http;
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
    public class BookController : Controller
    {
        #region fields
        private readonly BookAndMovieContext _context;
        private readonly IBookService _bookService;
        private readonly IUserService _userService;
        private readonly IUserBookService _userBookService;
        private readonly IMapper _mapper;

        #endregion fields

        #region ctor
        public BookController(BookAndMovieContext context, IBookService bookService, IMapper mapper, IUserBookService userBookService, IUserService userService)
        {
            _context = context;
            _bookService = bookService;
            _mapper = mapper;
            _userBookService = userBookService;
            _userService = userService;
        }
        #endregion ctor

        #region methods

        public async Task<IActionResult> Index()
        {
            var model = new BookApiModel();
            string clientUrl = $"{model.WebsiteRootUrl}?q=flowers&orderBy=newest&key={model.APIKey}&maxResults=20";
            var books = await model.GetBookFromGoogle(url: clientUrl);
            return View(model: books);
        }

        public async Task<IActionResult> GetWishlist()
        {
            var bookViewModel = await CreateBookModel(bookListType: "Wishlist", databaseSavingType: 1);
            return View("Books", bookViewModel);
        }

        public async Task<IActionResult> GetFinishedlist()
        {
            var bookViewModel = await CreateBookModel(bookListType: "Finishedlist", databaseSavingType: 2);
            return View("Books", bookViewModel);
        }

        public async Task<IActionResult> GetFavouriteBooks()
        {
            var bookViewModel = await CreateBookModel(bookListType: "Favouritelist", databaseSavingType: 3);
            return View("Books", bookViewModel);
        }

        private async Task<BookViewModel> CreateBookModel(string bookListType, int databaseSavingType)
        {
            var email = HttpContext.Session.GetString("email");
            if (email != null)
            {
                var user = _userService.GetAll(x => x.Email == email).SingleOrDefault();
                var userBooks = await _userBookService.GetAllAsync(x => x.UserId == user.Id && x.DatabaseSavingType == databaseSavingType);
                var books = _bookService.GetAll().Where(x => userBooks.Any(y => y.BookId == x.Id));
                var booksModel = books.Select(x => _mapper.Map<BookModel>(x)).ToList();
                var bookViewModel = new BookViewModel { Books = booksModel, BookListType = bookListType };
                return bookViewModel;
            }

            return null;
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchBook(string query)
        {
            if (!string.IsNullOrEmpty(query))
            {
                var model = new BookApiModel();
                string clientUrl = $"{model.WebsiteRootUrl}?key={model.APIKey}" + "&q=" + query + "+intitle:" + query;
                var books = await model.GetBookFromGoogle(url: clientUrl);
                return View("Search", books);
            }
            return null;
        }

        [HttpPost]
        public IActionResult BookDetail(BookModel book)
        {
            return View("Detail", model: book);
        }

        /// <summary>
        /// Add the book to Want to read table
        /// </summary>
        /// <param name="book">Book</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddBookToWishlist(BookModel book)
        {
            if (ModelState.IsValid)
            {
                await SaveBookToDatabase(model: book, 1);
            }
            return RedirectToAction("GetWishlist");
        }

        /// <summary>
        /// Add the book to Want to read table
        /// </summary>
        /// <param name="book">Book</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddBookToFinishedlist(BookModel book)
        {
            if (ModelState.IsValid)
            {
                await SaveBookToDatabase(model: book, 2);
            }
            return RedirectToAction("GetFinishedlist");
        }

        private async Task SaveBookToDatabase(BookModel model, int databaseSavingType)
        {
            var bookModel = _mapper.Map<Book>(model);
            bookModel.Id = 0;
            bookModel.Thumbnail = model.ImageLinks.Thumbnail;
            bookModel.SmallThumbnail = model.ImageLinks.SmallThumbnail;
            bookModel.Author = model.Authors[0];
            bookModel.Category = model.Categories[0];
            bookModel.DatabaseSavingType = databaseSavingType;
            await _bookService.AddAsync(bookModel);
            var email = HttpContext.Session.GetString("email");
            if (email != null)
            {
                var bookInDatabase = await _bookService.GetAllAsync(x => x.SmallThumbnail == model.ImageLinks.SmallThumbnail);
                var user = await _userService.GetAllAsync(x => x.Email == email);
                _userBookService.Add(new UserBook { BookId = bookInDatabase[0].Id, UserId = user[0].Id, DatabaseSavingType = databaseSavingType });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddBookToFavouritelist(BookModel book)
        {
            if (ModelState.IsValid)
            {
                var bookModel = _mapper.Map<Book>(book);
                bookModel.Id = 0;
                bookModel.DatabaseSavingType = 3;
                await _bookService.AddAsync(bookModel);
                var email = HttpContext.Session.GetString("email");
                if (email != null)
                {
                    var bookInDatabase = await _bookService.GetAllAsync(x => x.SmallThumbnail == book.SmallThumbnail);
                    var user = await _userService.GetAllAsync(x => x.Email == email);
                    _userBookService.Add(new UserBook { BookId = bookInDatabase[0].Id, UserId = user[0].Id, DatabaseSavingType = 3 });
                }
            }
            return RedirectToAction("GetFinishedlist");
        }

        [HttpPost]
        public async Task<IActionResult> MoveBookToFinishedlist(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book != null)
            {
                book.DatabaseSavingType = 2;
                await _bookService.UpdateAsync(book);
                var email = HttpContext.Session.GetString("email");
                if (email != null)
                {
                    var user = await _userService.GetAllAsync(x => x.Email == email);
                    var userBook =  _userBookService.GetAll(x => x.UserId == user[0].Id && x.DatabaseSavingType == 1 && x.BookId == book.Id).SingleOrDefault();
                    userBook.DatabaseSavingType = 2;
                    await _userBookService.UpdateAsync(userBook);
                }
            }
            return RedirectToAction("GetFinishedlist");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveBookFromWishlist(int id)
        {
            await RemoveBookFromDatabase(id);
            return RedirectToAction("GetWishlist");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveBookFromFinishedlist(int id)
        {
            await RemoveBookFromDatabase(id);
            return RedirectToAction("GetFinishedlist");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveBookFromFavouritelist(int id)
        {
            await RemoveBookFromDatabase(id);
            return RedirectToAction("GetFavouriteBooks");
        }

        private async Task RemoveBookFromDatabase(int id)
        {
            await _bookService.DeleteAsync(new Book { Id = id });
        }

        [HttpPost]
        public IActionResult RateBook(BookModel model)
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
        public async Task<IActionResult> Rate(float bookRateValue, int id)
        {
            var email = HttpContext.Session.GetString("email");
            if (email != null)
            {
                var user = await _userService.GetAllAsync(x => x.Email == email);
                var userBook = _userBookService.GetAll(x => x.BookId == id && x.DatabaseSavingType == 2 && x.UserId == user[0].Id).SingleOrDefault();
                userBook.Rating = bookRateValue;
                await _userBookService.UpdateAsync(entity: userBook);
                ViewBag.Rate = await GetMyRatings();
                return View("Rate");
            }
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRating(int id)
        {
            var email = HttpContext.Session.GetString("email");
            if (email != null)
            {
                var user = await _userService.GetAllAsync(x => x.Email == email);
                var userBooks = await _userBookService.GetAllAsync(x => x.Rating > 0 && x.UserId == user[0].Id && x.DatabaseSavingType == 2 && x.BookId == id);
                userBooks[0].Rating = 0;
                await _userBookService.UpdateAsync(entity: userBooks[0]);
                ViewBag.Rate = await GetMyRatings();
                return View("Rate");
            }

            return null;
        }

        private async Task<List<BookModel>> GetMyRatings()
        {
            var email = HttpContext.Session.GetString("email");
            if (email != null)
            {
                var user = await _userService.GetAllAsync(x => x.Email == email);
                var userBooks = await _userBookService.GetAllAsync(x => x.Rating > 0 && x.UserId == user[0].Id && x.DatabaseSavingType == 2);
                var books = _bookService.GetAll().Where(x => userBooks.Any(y => y.BookId == x.Id)).ToList();
                var bookModel = books.Select(x => _mapper.Map<BookModel>(x)).ToList();
                for (int i = 0; i < bookModel.Count; i++)
                {
                    bookModel[i].AverageRating = userBooks[i].Rating;
                }
                return bookModel;
            }
            return null;
        }

        #endregion methods

    }
}
