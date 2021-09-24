using BooksAndMovies.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndMovies.Business.Abstract
{
    public interface IBookService
    {
        WantToRead GetById(int id);
        List<WantToRead> GetAll();
        void Add(WantToRead entity);
        void Delete(WantToRead entity);
        void Update(WantToRead entity);

        Task<WantToRead> GetByIdAsync(int id);
        Task<List<WantToRead>> GetAllAsync();
        Task AddAsync(WantToRead entity);
        Task DeleteAsync(WantToRead entity);
        Task UpdateAsync(WantToRead entity);
    }
}
