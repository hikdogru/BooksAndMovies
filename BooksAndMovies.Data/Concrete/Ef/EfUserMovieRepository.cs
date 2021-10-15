using BooksAndMovies.Data.Abstract;
using BooksAndMovies.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndMovies.Data.Concrete.Ef
{
    public class EfUserMovieRepository : EfEntityRepositoryBase<UserMovie, BookAndMovieContext> , IUserMovieRepository
    {
        public EfUserMovieRepository(BookAndMovieContext context) : base(context)
        {
        }
    }
}
