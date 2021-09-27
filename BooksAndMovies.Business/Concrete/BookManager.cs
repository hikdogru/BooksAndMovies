using BooksAndMovies.Business.Abstract;
using BooksAndMovies.Data.Abstract;
using BooksAndMovies.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BooksAndMovies.Business.Concrete
{
    public class BookManager : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookManager(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public void Add(Book entity)
        {
            _bookRepository.Add(entity);
        }

        public async Task AddAsync(Book entity)
        {
            await _bookRepository.AddAsync(entity);
        }

        public void Delete(Book entity)
        {
            _bookRepository.Delete(entity);
        }

        public async Task DeleteAsync(Book entity)
        {
            await _bookRepository.DeleteAsync(entity);
        }

        public List<Book> GetAll(Expression<Func<Book, bool>> filter = null)
        {
           return filter == null ? _bookRepository.GetAll() : _bookRepository.GetAll(filter);
        }

        public async Task<List<Book>> GetAllAsync(Expression<Func<Book, bool>> filter = null)
        {
            return filter == null ? await _bookRepository.GetAllAsync() : await _bookRepository.GetAllAsync(filter);
        }

        public Book GetById(int id)
        {
            return _bookRepository.GetById(x => x.Id == id);
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _bookRepository.GetByIdAsync(x => x.Id == id);
        }

        public void Update(Book entity)
        {
            _bookRepository.Update(entity);
        }

        public async Task UpdateAsync(Book entity)
        {
            await _bookRepository.UpdateAsync(entity);
        }
    }
}
