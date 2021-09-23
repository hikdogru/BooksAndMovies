using BooksAndMovies.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndMovies.Entity
{
    public class Movie : Media
    {
        public string ReleaseDate { get; set; }
        public string Title { get; set; }
        public string OriginalTitle { get; set; }
    }
}
