using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAndMovies.WebUI.Models
{
    public class BookJsonModel
    {
        public int TotalItems { get; set; }
        public List<BookInfoModel> Items { get; set; }
    }
}
