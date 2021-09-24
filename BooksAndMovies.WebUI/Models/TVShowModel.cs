using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAndMovies.WebUI.Models
{
    public class TVShowModel:MediaModel
    {
        public string Name { get; set; }
        public string OriginalName { get; set; }
        public string FirstAirDate { get; set; }
        
    }
}
