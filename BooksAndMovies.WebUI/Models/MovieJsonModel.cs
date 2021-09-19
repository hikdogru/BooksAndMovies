using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAndMovies.WebUI.Models
{
    public class MovieJsonModel: JsonModel
    {
        public List<MovieModel> Results { get; set; }
    }
}
