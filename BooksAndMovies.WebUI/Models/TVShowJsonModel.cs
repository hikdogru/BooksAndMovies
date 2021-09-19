using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAndMovies.WebUI.Models
{
    public class TVShowJsonModel:JsonModel
    {
        public List<TVShowModel> Results { get; set; }

    }
}
