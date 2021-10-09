using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksAndMovies.WebUI.Models
{
    public class MediaModel
    {
        public int Id { get; set; }
        public int RealId { get; set; }
        /// <summary>
        /// Media's image url
        /// </summary>
        public string PosterPath { get; set; }
        public string OriginalLanguage { get; set; }
        public double Popularity { get; set; }
        public string BackdropPath { get; set; }
        public int VoteCount { get; set; }
        public double VoteAverage { get; set; }
        public double Rating { get; set; }
        public string Overview { get; set; }
        public int DatabaseSavingType { get; set; }
    }
}
