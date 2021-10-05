using System;
using System.Collections.Generic;
using BooksAndMovies.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace BooksAndMovies.Business.Abstract
{
    public interface IMovieService
    {
        Movie GetById(int id);
        List<Movie> GetAll(Expression<Func<Movie, bool>> filter = null);
        void Add(Movie entity);
        void Delete(Movie entity);
        void Update(Movie entity);
        bool IsMovieExistInDatabase(Movie entity, int databaseSaveType);


        Task<Movie> GetByIdAsync(int id);
        Task<List<Movie>> GetAllAsync(Expression<Func<Movie, bool>> filter = null);
        Task AddAsync(Movie entity);
        Task DeleteAsync(Movie entity);
        Task UpdateAsync(Movie entity);
        Task<bool> IsMovieExistInDatabaseAsync(Movie entity, int databaseSaveType);

    }
}
