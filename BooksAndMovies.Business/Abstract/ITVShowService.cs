using BooksAndMovies.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndMovies.Business.Abstract
{
    public interface ITVShowService
    {
        TVShow GetById(int id);
        List<TVShow> GetAll(Expression<Func<TVShow, bool>> filter = null);
        void Add(TVShow entity);
        void Delete(TVShow entity);
        void Update(TVShow entity);

        Task<TVShow> GetByIdAsync(int id);
        Task<List<TVShow>> GetAllAsync(Expression<Func<TVShow, bool>> filter = null);
        Task AddAsync(TVShow entity);
        Task DeleteAsync(TVShow entity);
        Task UpdateAsync(TVShow entity);
    }
}
