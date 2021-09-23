using BooksAndMovies.Business.Abstract;
using BooksAndMovies.Data.Abstract;
using BooksAndMovies.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BooksAndMovies.Business.Concrete
{
    public class WantToReadManager : IWantToReadService
    {
        private readonly IWantToReadRepository _wantToReadRepository;

        public WantToReadManager(IWantToReadRepository wantToReadRepository)
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
            throw new NotImplementedException();
        }

        public Task DeleteAsync(WantToRead entity)
        {
            throw new NotImplementedException();
        }

        public List<WantToRead> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<WantToRead>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public WantToRead GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<WantToRead> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(WantToRead entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(WantToRead entity)
        {
            throw new NotImplementedException();
        }
    }
}
