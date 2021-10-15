using BooksAndMovies.Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndMovies.Entity
{
    public class UserMovie : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public int DatabaseSavingType { get; set; }
        public User User { get; set; }
        public Movie Movie { get; set; }
    }
}
