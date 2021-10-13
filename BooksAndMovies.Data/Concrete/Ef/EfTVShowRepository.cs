using BooksAndMovies.Data.Abstract;
using BooksAndMovies.Entity;

namespace BooksAndMovies.Data.Concrete.Ef
{
    public class EfTVShowRepository : EfEntityRepositoryBase<TVShow, BookAndMovieContext> , ITVShowRepository
    {
        public EfTVShowRepository(BookAndMovieContext context) : base(context: context)
        {

        }

    }
}
