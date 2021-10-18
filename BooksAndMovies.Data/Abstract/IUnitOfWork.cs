using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndMovies.Data.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IBookRepository Books { get; }
        IMovieRepository Movies { get; }
        ITVShowRepository TVShows { get; }
        IUserRepository Users { get; }
        IUserMovieRepository UserMovies { get; }
        IUserTVShowRepository UserTVShows { get; }
        IUserBookRepository UserBooks { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
