using BooksAndMovies.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndMovies.Data.Concrete.Ef
{
    public class BookAndMovieContext : DbContext
    {
        public DbSet<WantToRead> WantToReads { get; set; }
        //public DbSet<WatchList> WatchLists { get; set; }
        //public DbSet<Movie> Movies { get; set; }
        //public DbSet<TVShow> TVShows { get; set; }
        //public DbSet<WishList> WishLists { get; set; }


        public BookAndMovieContext(DbContextOptions<BookAndMovieContext> options) : base(options: options)
        {

        }

        public BookAndMovieContext()
        {

        }

    }
}
