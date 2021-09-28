using BooksAndMovies.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAndMovies.WebUI.ViewModels
{
    public class BookViewModel
    {
        public List<Book> Books { get; set; }
        /// <summary>
        /// Wishlist or FinishedList
        /// </summary>
        public string BookListType { get; set; }
    }
}
