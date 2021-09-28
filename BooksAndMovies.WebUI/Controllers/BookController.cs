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
        private readonly BookAndMovieContext _context;
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BookController(BookAndMovieContext context, IBookService bookService, IMapper mapper)
        {
            _context = context;
            _bookService = bookService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetWishlist()
        {
            var bookWishList = await _bookService.GetAllAsync(x => x.DatabaseSavingType == 1);
            var bookViewModel = new BookViewModel { Books = bookWishList, BookListType = "Wishlist" };
            return View("Books", bookViewModel);
        }

        public async Task<IActionResult> GetFinishedlist()
        {
            var bookFinishedlist = await _bookService.GetAllAsync(x => x.DatabaseSavingType == 2);
            var bookViewModel = new BookViewModel { Books = bookFinishedlist, BookListType = "Finishedlist" };
            return View("Books", bookViewModel);
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
                var bookModel = _mapper.Map<Book>(book);
                bookModel.Thumbnail = book.ImageLinks.Thumbnail;
                bookModel.SmallThumbnail = book.ImageLinks.SmallThumbnail;
                bookModel.Author = book.Authors[0];
                bookModel.Category = book.Categories[0];
                bookModel.DatabaseSavingType = 1;
                await _bookService.AddAsync(bookModel);

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
                var bookModel = _mapper.Map<Book>(book);
                bookModel.Thumbnail = book.ImageLinks.Thumbnail;
                bookModel.SmallThumbnail = book.ImageLinks.SmallThumbnail;
                bookModel.Author = book.Authors[0];
                bookModel.Category = book.Categories[0];
                bookModel.DatabaseSavingType = 2;
                await _bookService.AddAsync(bookModel);

            }
            return RedirectToAction("GetFinishedlist");
        }

        [HttpPost]
        public async Task<IActionResult> MoveBookToFinishedlist(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if(book != null)
            {
                book.DatabaseSavingType = 2;
                await _bookService.UpdateAsync(book);
            }
            return RedirectToAction("GetFinishedlist");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveBookFromWishlist(int id)
        {
            await _bookService.DeleteAsync(new Book { Id = id });
            return RedirectToAction("GetWishlist");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveBookFromFinishedlist(int id)
        {
            await _bookService.DeleteAsync(new Book { Id = id });
            return RedirectToAction("GetFinishedlist");
        }
    }
}
