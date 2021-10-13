using BooksAndMovies.Data.Abstract;
using BooksAndMovies.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndMovies.Data.Concrete.Ef
{
    public class EfUserRepository : EfEntityRepositoryBase<User, BookAndMovieContext>, IUserRepository
    {
        public EfUserRepository(BookAndMovieContext context) : base(context)
        {
        }
    }
}
