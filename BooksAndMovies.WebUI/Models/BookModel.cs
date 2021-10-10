using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAndMovies.WebUI.Models
{
    public class BookModel
    {
        private string _title;

        public string Title
        {
            get => _title.Length < 20 ? _title : _title.Substring(0, 20) + "...";
            set => _title = value;
        }

        public List<string> Authors { get; set; }
        public List<string> Categories { get; set; }
        public string Publisher { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public double AverageRating { get; set; }
        public BookImageModel ImageLinks { get; set; }
        public string Thumbnail { get; set; }
        public string SmallThumbnail { get; set; }
    }
}
