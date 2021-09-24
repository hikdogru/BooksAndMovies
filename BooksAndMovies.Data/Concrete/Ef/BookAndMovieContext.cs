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
        public DbSet<MovieWatchList> MovieWatchLists { get; set; }
        public DbSet<TVShowWatchList> TVShowWatchLists { get; set; }


        public BookAndMovieContext(DbContextOptions<BookAndMovieContext> options) : base(options: options)
        {

        }

        public BookAndMovieContext()
        {

        }

    }
}
