using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAndMovies.WebUI.Models
{
    public class BookModel
    {
        public string Title { get; set; }
        public List<string> Authors { get; set; }
        public List<string> Categories { get; set; }
        public string Publisher { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public double AverageRating { get; set; }
        public BookImageModel ImageLinks { get; set; }
    }
}
