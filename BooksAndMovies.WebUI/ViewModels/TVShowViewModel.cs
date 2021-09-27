using BooksAndMovies.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAndMovies.WebUI.ViewModels
{
    public class TVShowViewModel
    {
        public List<TVShow> TVShows { get; set; }
        /// <summary>
        /// Wishlist or Watchedlist
        /// </summary>
        public string TVShowListType { get; set; }
    }
}
