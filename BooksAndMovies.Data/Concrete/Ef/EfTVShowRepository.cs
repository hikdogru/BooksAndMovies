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
    public class EfTVShowRepository : EfEntityRepositoryBase<TVShowWatchList, BookAndMovieContext> , ITVShowRepository
    {
        public EfTVShowRepository(BookAndMovieContext context) : base(context: context)
        {

        }

        public void Add(TVShow entity)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(TVShow entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(TVShow entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(TVShow entity)
        {
            throw new NotImplementedException();
        }

        public List<TVShow> GetAll(Expression<Func<TVShow, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public Task<List<TVShow>> GetAllAsync(Expression<Func<TVShow, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public TVShow GetById(Expression<Func<TVShow, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Task<TVShow> GetByIdAsync(Expression<Func<TVShow, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Update(TVShow entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TVShow entity)
        {
            throw new NotImplementedException();
        }
    }
}
