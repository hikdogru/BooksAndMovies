
using BooksAndMovies.Business.Abstract;
using BooksAndMovies.Data.Abstract;
using BooksAndMovies.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndMovies.Business.Concrete
{
    public class UserBookManager : IUserBookService
    {
        private readonly IUserBookRepository _userbookRepository;

        public UserBookManager(IUserBookRepository userRepository)
        {
            _userbookRepository = userRepository;
        }

        public void Add(UserBook entity)
        {
            _userbookRepository.Add(entity);
        }

        public async Task AddAsync(UserBook entity)
        {
            await _userbookRepository.AddAsync(entity);
        }

        public void Delete(UserBook entity)
        {
            _userbookRepository.Delete(entity);
        }

        public async Task DeleteAsync(UserBook entity)
        {
            await _userbookRepository.DeleteAsync(entity);
        }

        public List<UserBook> GetAll(Expression<Func<UserBook, bool>> filter = null)
        {
            return filter == null ? _userbookRepository.GetAll() : _userbookRepository.GetAll(filter);
        }

        public async Task<List<UserBook>> GetAllAsync(Expression<Func<UserBook, bool>> filter = null)
        {
            return filter == null ? await _userbookRepository.GetAllAsync() : await _userbookRepository.GetAllAsync(filter);
        }

        public UserBook GetById(int id)
        {
            var UserBook = _userbookRepository.GetById(x => x.Id == id);
            return UserBook;
        }

        public async Task<UserBook> GetByIdAsync(int id)
        {
            var UserBook = await _userbookRepository.GetByIdAsync(x => x.Id == id);
            return UserBook;
        }


        public void Update(UserBook entity)
        {
            _userbookRepository.Update(entity);
        }

        public async Task UpdateAsync(UserBook entity)
        {
            await _userbookRepository.UpdateAsync(entity);
        }
    }
}
