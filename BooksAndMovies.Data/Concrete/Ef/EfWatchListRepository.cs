using BooksAndMovies.Core.Data.Concrete.Ef;
using BooksAndMovies.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndMovies.Data.Concrete.Ef
{
    public class EfWatchListRepository : EfEntityRepositoryBase<WatchList, BookAndMovieContext>
    {
        public EfWatchListRepository(BookAndMovieContext context) : base(context)
        {
        }
    }
}
