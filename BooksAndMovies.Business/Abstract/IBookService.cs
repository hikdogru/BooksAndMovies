using BooksAndMovies.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndMovies.Business.Abstract
{
    public interface IBookService
    {
        Book GetById(int id);
        List<Book> GetAll(Expression<Func<Book, bool>> filter = null);
        void Add(Book entity);
        void Delete(Book entity);
        void Update(Book entity);
        bool IsBookExistInDatabase(Book entity, int databaseSaveType);


        Task<Book> GetByIdAsync(int id);
        Task<List<Book>> GetAllAsync(Expression<Func<Book, bool>> filter = null);
        Task AddAsync(Book entity);
        Task DeleteAsync(Book entity);
        Task UpdateAsync(Book entity);
        Task<bool> IsBookExistInDatabaseAsync(Book entity, int databaseSaveType);

    }
}
