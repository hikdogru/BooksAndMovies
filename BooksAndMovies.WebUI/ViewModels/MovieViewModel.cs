using BooksAndMovies.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAndMovies.WebUI.ViewModels
{
    public class MovieViewModel
    {
        public List<Movie> Movies { get; set; }
        /// <summary>
        /// Wishlist or Watchedlist
        /// </summary>
        public string MovieListType { get; set; }
    }
}
