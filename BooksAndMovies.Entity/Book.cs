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
        public int Id { get; set; }
        public string UniqueId { get; set; }
        public string Title { get; set; }
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
