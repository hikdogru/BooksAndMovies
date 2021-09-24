using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAndMovies.WebUI.Models
{
    public class MediaModel
    {
        
        /// <summary>
        /// Media's image url
        /// </summary>
        public string PosterPath { get; set; }
        public string OriginalLanguage { get; set; }
        public double Popularity { get; set; }
        public int GenreId { get; set; }
        public string BackdropPath { get; set; }
        public int VoteCount { get; set; }
        public double VoteAverage { get; set; }
        public string Overview { get; set; }
    }
}
