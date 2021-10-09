using BooksAndMovies.Entity;
using BooksAndMovies.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAndMovies.WebUI.ViewModels
{
    public class TVShowViewModel
    {
        public List<TVShowModel> TVShows { get; set; }
        /// <summary>
        /// Wishlist or Watchedlist
        /// </summary>
        public string TVShowListType { get; set; }
    }
}
