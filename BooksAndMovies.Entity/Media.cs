using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndMovies.Entity
{
    public class Media
    {
        private string _rootImageUrl = "https://image.tmdb.org/t/p/w500";
        private string _imageUrl;
        public int Id { get; set; }
        /// <summary>
        /// Media's image url
        /// </summary>
        public string PosterPath
        {
            get => _imageUrl;
            set => _imageUrl = _rootImageUrl + value;
        }
        public string OriginalLanguage { get; set; }
        public double Popularity { get; set; }
        public string BackdropPath
        {
            get => _imageUrl;
            set => _imageUrl = _rootImageUrl + value;
        }
        public int VoteCount { get; set; }
        public double VoteAverage { get; set; }
        public string Overview { get; set; }
        /// <summary>
        /// 1) WatchList 2) WatchedList
        /// </summary>
        public int DatabaseSavingType { get; set; }
    }
}
