using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndMovies.Entity
{
    public class TVShow : Media
    {
        public string Name { get; set; }
        public string OriginalName { get; set; }
        public string FirstAirDate { get; set; }
    }
}
