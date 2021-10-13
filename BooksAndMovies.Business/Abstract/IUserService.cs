using BooksAndMovies.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndMovies.Business.Abstract
{
    public interface IUserService
    {
        User GetById(int id);
        List<User> GetAll(Expression<Func<User, bool>> filter = null);
        void Add(User entity);
        void Delete(User entity);
        void Update(User entity);
        bool IsUserExistInDatabase(User entity, int databaseSaveType);


        Task<User> GetByIdAsync(int id);
        Task<List<User>> GetAllAsync(Expression<Func<User, bool>> filter = null);
        Task AddAsync(User entity);
        Task DeleteAsync(User entity);
        Task UpdateAsync(User entity);
        Task<bool> IsUserExistInDatabaseAsync(User entity, int databaseSaveType);
    }
}
