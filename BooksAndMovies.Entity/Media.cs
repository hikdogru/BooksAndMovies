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
        private string _posterImageUrl;
        private string _backdropImageUrl;

        public int Id { get; set; }
        /// <summary>
        /// Media's image url
        /// </summary>
        public string PosterPath
        {
            get => _posterImageUrl;
            set
            {
                if (value.Contains(_rootImageUrl) == false)
                    _posterImageUrl = _rootImageUrl + value;
                else
                    _posterImageUrl = value;
            }
        }
        public string OriginalLanguage { get; set; }
        public double Popularity { get; set; }
        public string BackdropPath
        {
            get => _backdropImageUrl;
            set
            {

                if (value.Contains(_rootImageUrl) == false)
                    _backdropImageUrl = _rootImageUrl + value;
                else
                    _backdropImageUrl = value;
            }
         }
    
    public int VoteCount { get; set; }
    public double VoteAverage { get; set; }
    public string Overview { get; set; }
    /// <summary>
    /// 1) WatchList 2) WatchedList 3) Favouritelist
    /// </summary>
    public int DatabaseSavingType { get; set; }
}
}
