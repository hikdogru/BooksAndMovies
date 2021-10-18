using BooksAndMovies.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndMovies.Data.Concrete.Ef
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BookAndMovieContext _context;

        public UnitOfWork(BookAndMovieContext context)
        {
            _context = context;
        }

        private EfBookRepository _bookRepository;
        private EfMovieRepository _movieRepository;
        private EfTVShowRepository _tvShowRepository;
        private EfUserRepository _userRepository;
        private EfUserMovieRepository _userMovieRepository;
        private EfUserTVShowRepository _userTVShowRepository;
        private EfUserBookRepository _userBookRepository;

        public IBookRepository Books => _bookRepository = _bookRepository ?? new EfBookRepository(context: _context);
        public IMovieRepository Movies => _movieRepository = _movieRepository ?? new EfMovieRepository(context: _context);
        public ITVShowRepository TVShows => _tvShowRepository = _tvShowRepository ?? new EfTVShowRepository(context: _context);
        public IUserRepository Users => _userRepository = _userRepository ?? new EfUserRepository(context: _context);
        public IUserMovieRepository UserMovies => _userMovieRepository = _userMovieRepository ?? new EfUserMovieRepository(context: _context);
        public IUserTVShowRepository UserTVShows => _userTVShowRepository = _userTVShowRepository ?? new EfUserTVShowRepository(context: _context);
        public IUserBookRepository UserBooks => _userBookRepository = _userBookRepository ?? new EfUserBookRepository(context: _context);

        public void Dispose()
        {
            _context.Dispose();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
