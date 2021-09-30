using BooksAndMovies.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndMovies.Entity
{
    public class Book : IEntity
    {
        private string _title;

        public int Id { get; set; }
        public string UniqueId { get; set; }
        public string Title
        {
            get => _title.Length < 20 ? _title : _title.Substring(0, 20) + "...";
            set => _title = value;
        }


        public string Author { get; set; }
        public string Category { get; set; }
        public string Publisher { get; set; }
        public string Description { get; set; }
        public int PageCount { get; set; }
        public double AverageRating { get; set; }
        public string Thumbnail { get; set; }
        public string SmallThumbnail { get; set; }
        public int DatabaseSavingType { get; set; }
    }
}
