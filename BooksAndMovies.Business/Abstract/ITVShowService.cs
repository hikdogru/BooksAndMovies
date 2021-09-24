using BooksAndMovies.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndMovies.Business.Abstract
{
    public interface ITVShowService
    {
        TVShowWatchList GetById(int id);
        List<TVShowWatchList> GetAll();
        void Add(TVShowWatchList entity);
        void Delete(TVShowWatchList entity);
        void Update(TVShowWatchList entity);

        Task<TVShowWatchList> GetByIdAsync(int id);
        Task<List<TVShowWatchList>> GetAllAsync();
        Task AddAsync(TVShowWatchList entity);
        Task DeleteAsync(TVShowWatchList entity);
        Task UpdateAsync(TVShowWatchList entity);
    }
}
