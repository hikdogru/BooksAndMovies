using AutoMapper;
using BooksAndMovies.Business.Abstract;
using BooksAndMovies.Data;
using BooksAndMovies.Data.Concrete.Ef;
using BooksAndMovies.Entity;
using BooksAndMovies.WebUI.Models;
using BooksAndMovies.WebUI.Models.GoogleApi;
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
    public class BookController : Controller
    {
        #region fields
        private readonly BookAndMovieContext _context;
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        #endregion fields

        #region ctor
        public BookController(BookAndMovieContext context, IBookService bookService, IMapper mapper)
        {
            _context = context;
            _bookService = bookService;
            _mapper = mapper;
        }
        #endregion ctor

        #region methods

        public async Task<IActionResult> Index()
        {
            string clientUrl = "https://www.googleapis.com/books/v1/volumes?q=flowers&orderBy=newest&key=AIzaSyAWeKsrZKQlLMC2AaDxM1zRbLoBHoEMj8w&maxResults=20";
            var books = await new BookApiModel().GetBookFromGoogle(url: clientUrl);
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
            var books = await _bookService.GetAllAsync(x => x.DatabaseSavingType == databaseSavingType);
            var booksModel = books.Select(x => _mapper.Map<BookModel>(x)).ToList();
            var bookViewModel = new BookViewModel { Books = booksModel, BookListType = bookListType };
            return bookViewModel;
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
                string clientUrl = "https://www.googleapis.com/books/v1/volumes?key=AIzaSyAWeKsrZKQlLMC2AaDxM1zRbLoBHoEMj8w" + "&q=" + query;
                var books = await new BookApiModel().GetBookFromGoogle(url: clientUrl);
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
            bookModel.Thumbnail = model.ImageLinks.Thumbnail;
            bookModel.SmallThumbnail = model.ImageLinks.SmallThumbnail;
            bookModel.Author = model.Authors[0];
            bookModel.Category = model.Categories[0];
            bookModel.DatabaseSavingType = databaseSavingType;
            await _bookService.AddAsync(bookModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddBookToFavouritelist(Book book)
        {
            if (ModelState.IsValid)
            {
                book.DatabaseSavingType = 3;
                await _bookService.AddAsync(book);
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
            return RedirectToAction("GetFinishedlist");
        }

        private async Task RemoveBookFromDatabase(int id)
        {
            await _bookService.DeleteAsync(new Book { Id = id });
        }

        #endregion methods
    }
}
