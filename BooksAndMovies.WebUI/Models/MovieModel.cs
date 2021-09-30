using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAndMovies.WebUI.Models
{
    public class MovieModel : MediaModel
    {
        private string _title = "";
        public string ReleaseDate { get; set; }
        public string Title
        {
            get => _title.Length < 20 ? _title : _title.Substring(0, 20) + "...";
            set => _title = value;
        }
        public string OriginalTitle { get; set; }
        
    }
}
