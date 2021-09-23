using BooksAndMovies.Data;
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

        public BookController(BookAndMovieContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchBook(string query)
        {
            if (!string.IsNullOrEmpty(query))
            {
                string clientUrl = "https://www.googleapis.com/books/v1/volumes?key=AIzaSyAWeKsrZKQlLMC2AaDxM1zRbLoBHoEMj8w&maxResults=5" + "&q=" + query;
                var books = await new BookApiModel().GetBookFromGoogle(url: clientUrl);


               await _context.Books.AddAsync(new Entity.Book
                {
                    Author = books[0].VolumeInfo.Authors[0],
                    AverageRating = books[0].VolumeInfo.AverageRating,
                    Category = books[0].VolumeInfo.Categories[0],
                    Description = books[0].VolumeInfo.Description,
                    Publisher = books[0].VolumeInfo.Publisher,
                    Title = books[0].VolumeInfo.Title,
                    PageCount = books[0].VolumeInfo.PageCount,
                    SmallThumbnail = books[0].VolumeInfo.ImageLinks.SmallThumbnail,
                    Thumbnail = books[0].VolumeInfo.ImageLinks.Thumbnail
                });
               await _context.SaveChangesAsync();
                return View("Search", books);
            }

            return null;

        }
    }
}
