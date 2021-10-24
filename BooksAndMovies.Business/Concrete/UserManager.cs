using BooksAndMovies.Business.Abstract;
using BooksAndMovies.Data.Abstract;
using BooksAndMovies.Entity;
using Microsoft.AspNetCore.Http;
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
        private readonly IUnitOfWork _unitOfWork;

        public UserManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public void Add(User entity)
        {
            if (IsUserExistInDatabase(entity) == false)
            {
                _unitOfWork.Users.Add(entity);
                _unitOfWork.SaveChanges();

            }
        }

        public async Task AddAsync(User entity)
        {
            if (await IsUserExistInDatabaseAsync(entity) == false)
            {
                await _unitOfWork.Users.AddAsync(entity);
                await _unitOfWork.SaveChangesAsync();

            }
        }

        public void Delete(User entity)
        {
            _unitOfWork.Users.Delete(entity);
            _unitOfWork.SaveChanges();

        }

        public async Task DeleteAsync(User entity)
        {
            await _unitOfWork.Users.DeleteAsync(entity);
            await _unitOfWork.SaveChangesAsync();

        }

        public List<User> GetAll(Expression<Func<User, bool>> filter = null)
        {
            return filter == null ? _unitOfWork.Users.GetAll() : _unitOfWork.Users.GetAll(filter);
        }

        public async Task<List<User>> GetAllAsync(Expression<Func<User, bool>> filter = null)
        {
            return filter == null ? await _unitOfWork.Users.GetAllAsync() : await _unitOfWork.Users.GetAllAsync(filter);
        }

        public User GetById(int id)
        {
            var user = _unitOfWork.Users.GetById(x => x.Id == id);
            return user;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(x => x.Id == id);
            return user;
        }

        public bool IsUserExistInDatabase(User entity)
        {
            var user = _unitOfWork.Users.GetAll(x => x.Email == entity.Email);
            if (user.Count > 0)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> IsUserExistInDatabaseAsync(User entity)
        {
            var user = await _unitOfWork.Users.GetAllAsync(x => x.Email == entity.Email);
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
                _unitOfWork.Users.Update(entity);
                _unitOfWork.SaveChanges();

            }
        }

        public async Task UpdateAsync(User entity)
        {
            if (await IsUserExistInDatabaseAsync(entity))
            {
                await _unitOfWork.Users.UpdateAsync(entity);
                await _unitOfWork.SaveChangesAsync();
            }
        }

       
    }
}
