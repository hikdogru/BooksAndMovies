using BooksAndMovies.Core.Data.Concrete.Ef;
using BooksAndMovies.Data.Abstract;
using BooksAndMovies.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndMovies.Data.Concrete.Ef
{
    public class EfWantToReadRepository : EfEntityRepositoryBase<WantToRead, BookAndMovieContext>, IWantToReadRepository
    {
        public EfWantToReadRepository(BookAndMovieContext context) : base(context)
        {
        }
    }
}
