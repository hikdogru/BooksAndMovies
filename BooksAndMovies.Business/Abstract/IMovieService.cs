using System;
using System.Collections.Generic;
using BooksAndMovies.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndMovies.Business.Abstract
{
    public interface IMovieService
    {
        MovieWatchList GetById(int id);
        List<MovieWatchList> GetAll();
        void Add(MovieWatchList entity);
        void Delete(MovieWatchList entity);
        void Update(MovieWatchList entity);

        Task<MovieWatchList> GetByIdAsync(int id);
        Task<List<MovieWatchList>> GetAllAsync();
        Task AddAsync(MovieWatchList entity);
        Task DeleteAsync(MovieWatchList entity);
        Task UpdateAsync(MovieWatchList entity);
    }
}
