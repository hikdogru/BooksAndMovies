using BooksAndMovies.Data.Abstract;
using BooksAndMovies.Entity;

namespace BooksAndMovies.Data.Concrete.Ef
{
    public class EfBookRepository : EfEntityRepositoryBase<Book, BookAndMovieContext>, IBookRepository
    {
        public EfBookRepository(BookAndMovieContext context) : base(context)
        {
        }
    }
}
