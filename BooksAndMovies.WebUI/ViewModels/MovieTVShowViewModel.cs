using BooksAndMovies.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAndMovies.WebUI.ViewModels
{
    public class MovieTVShowViewModel
    {
        public List<MovieModel> Movies { get; set; }
        public List<TVShowModel> TVShows { get; set; }
    }
}
