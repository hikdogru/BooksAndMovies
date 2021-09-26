using AutoMapper;
using BooksAndMovies.Business.Abstract;
using BooksAndMovies.Data;
using BooksAndMovies.Data.Concrete.Ef;
using BooksAndMovies.Entity;
using BooksAndMovies.WebUI.Models;
using BooksAndMovies.WebUI.Models.GoogleApi;
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

        public async Task<IActionResult> GetWishList()
        {
            var bookWishList = await _bookService.GetAllAsync();
            return View("WishList", bookWishList);
        }

        [HttpPost]
        public async Task<IActionResult> SearchBook(string query)
        {
            if (!string.IsNullOrEmpty(query))
            {
                string clientUrl = "https://www.googleapis.com/books/v1/volumes?key=AIzaSyAWeKsrZKQlLMC2AaDxM1zRbLoBHoEMj8w&filter=full&maxResults=10" + "&q=intitle:" + query;
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
        public async Task<IActionResult> AddBookToWishList(BookModel book)
        {
            if (ModelState.IsValid)
            {
                var bookModel = _mapper.Map<WantToRead>(book);
                bookModel.Thumbnail = book.ImageLinks.Thumbnail;
                bookModel.SmallThumbnail = book.ImageLinks.SmallThumbnail;
                bookModel.Author = book.Authors[0];
                bookModel.Category = book.Categories[0];
                await _bookService.AddAsync(bookModel);

            }
            return View("GetWishList");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveBookFromWishList(int id)
        {
            await _bookService.DeleteAsync(new WantToRead { Id = id });
            return RedirectToAction("GetWishList");
        }
    }
}
