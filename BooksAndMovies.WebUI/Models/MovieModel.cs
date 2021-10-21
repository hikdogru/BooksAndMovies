using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAndMovies.WebUI.Models
{
    public class MovieModel : MediaModel
    {
        private string _title = "";
        private string _date;
        public string ReleaseDate
        {
            get => _date;
            set => _date = value.Length > 4 ? DateTime.Parse(value, new CultureInfo("en-US")).Year.ToString() : value.ToString();
        }
        public string Title
        {
            get => _title.Length < 20 ? _title : _title.Substring(0, 20) + "...";
            set => _title = value;
        }
        public string OriginalTitle { get; set; }

    }
}
