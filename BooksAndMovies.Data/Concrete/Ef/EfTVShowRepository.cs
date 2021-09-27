using BooksAndMovies.Core.Data.Concrete.Ef;
using BooksAndMovies.Data.Abstract;
using BooksAndMovies.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndMovies.Data.Concrete.Ef
{
    public class EfTVShowRepository : EfEntityRepositoryBase<TVShow, BookAndMovieContext> , ITVShowRepository
    {
        public EfTVShowRepository(BookAndMovieContext context) : base(context: context)
        {

        }

    }
}
