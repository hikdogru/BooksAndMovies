using BooksAndMovies.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndMovies.Business.Abstract
{
    public interface IUserBookService
    {
        UserBook GetById(int id);
        List<UserBook> GetAll(Expression<Func<UserBook, bool>> filter = null);
        void Add(UserBook entity);
        void Delete(UserBook entity);
        void Update(UserBook entity);


        Task<UserBook> GetByIdAsync(int id);
        Task<List<UserBook>> GetAllAsync(Expression<Func<UserBook, bool>> filter = null);
        Task AddAsync(UserBook entity);
        Task DeleteAsync(UserBook entity);
        Task UpdateAsync(UserBook entity);
    }
}
