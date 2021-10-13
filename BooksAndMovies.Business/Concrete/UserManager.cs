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
    public class UserManager : IUserService
    {
        private IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Add(User entity)
        {
            if (IsUserExistInDatabase(entity) == false)
            {
                _userRepository.Add(entity);

            }
        }

        public async Task AddAsync(User entity)
        {
            if (await IsUserExistInDatabaseAsync(entity) == false)
            {
                await _userRepository.AddAsync(entity);
            }
        }

        public void Delete(User entity)
        {
            _userRepository.Delete(entity);
        }

        public async Task DeleteAsync(User entity)
        {
            await _userRepository.DeleteAsync(entity);
        }

        public List<User> GetAll(Expression<Func<User, bool>> filter = null)
        {
            return filter == null ? _userRepository.GetAll() : _userRepository.GetAll(filter);
        }

        public async Task<List<User>> GetAllAsync(Expression<Func<User, bool>> filter = null)
        {
            return filter == null ? await _userRepository.GetAllAsync() : await _userRepository.GetAllAsync(filter);
        }

        public User GetById(int id)
        {
            var user = _userRepository.GetById(x => x.Id == id);
            return user;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(x => x.Id == id);
            return user;
        }

        public bool IsUserExistInDatabase(User entity)
        {
            var user = _userRepository.GetAll(x => x.Email == entity.Email);
            if (user.Count > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> IsUserExistInDatabaseAsync(User entity)
        {
            var user = await _userRepository.GetAllAsync(x => x.Email == entity.Email);
            if (user != null)
            {
                return true;
            }
            return false;
        }

        public void Update(User entity)
        {
            if (IsUserExistInDatabase(entity))
            {
                _userRepository.Update(entity);
            }
        }

        public async Task UpdateAsync(User entity)
        {
            if (await IsUserExistInDatabaseAsync(entity))
            {
                await _userRepository.UpdateAsync(entity);
            }
        }
    }
}
