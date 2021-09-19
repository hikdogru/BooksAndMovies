using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAndMovies.WebUI.Models
{
    public class MovieModel:MediaModel
    {
        public string ReleaseDate { get; set; }
        public string Title { get; set; }
        public string OriginalTitle { get; set; }
        public bool Video { get; set; }
        public bool Adult { get; set; }
    }
}
