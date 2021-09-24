using BooksAndMovies.Business.Abstract;
using BooksAndMovies.Data.Abstract;
using BooksAndMovies.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksAndMovies.Business.Concrete
{
    public class BookManager : IBookService
    {
        private readonly IBookRepository _wantToReadRepository;

        public BookManager(IBookRepository wantToReadRepository)
        {
            _wantToReadRepository = wantToReadRepository;
        }
        public void Add(WantToRead entity)
        {
            _wantToReadRepository.Add(entity);
        }

        public async Task AddAsync(WantToRead entity)
        {
            await _wantToReadRepository.AddAsync(entity);
        }

        public void Delete(WantToRead entity)
        {
            _wantToReadRepository.Delete(entity);
        }

        public async Task DeleteAsync(WantToRead entity)
        {
            await _wantToReadRepository.DeleteAsync(entity);
        }

        public List<WantToRead> GetAll()
        {
           return _wantToReadRepository.GetAll();
        }

        public async Task<List<WantToRead>> GetAllAsync()
        {
            return await _wantToReadRepository.GetAllAsync();
        }

        public WantToRead GetById(int id)
        {
            return _wantToReadRepository.GetById(x => x.Id == id);
        }

        public async Task<WantToRead> GetByIdAsync(int id)
        {
            return await _wantToReadRepository.GetByIdAsync(x => x.Id == id);
        }

        public void Update(WantToRead entity)
        {
            _wantToReadRepository.Update(entity);
        }

        public async Task UpdateAsync(WantToRead entity)
        {
            await _wantToReadRepository.UpdateAsync(entity);
        }
    }
}
