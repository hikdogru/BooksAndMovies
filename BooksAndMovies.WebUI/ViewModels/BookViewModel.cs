using BooksAndMovies.Entity;
using BooksAndMovies.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAndMovies.WebUI.ViewModels
{
    public class BookViewModel
    {
        public List<BookModel> Books { get; set; }
        /// <summary>
        /// Wishlist, FinishedList or FavouriteList
        /// </summary>
        public string BookListType { get; set; }
    }
}
