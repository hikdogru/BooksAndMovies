
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
        private readonly IUnitOfWork _unitOfWork;

        public UserBookManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }


        public void Add(UserBook entity)
        {
            _unitOfWork.UserBooks.Add(entity);
            _unitOfWork.SaveChanges();
        }

        public async Task AddAsync(UserBook entity)
        {
            await _unitOfWork.UserBooks.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

        }

        public void Delete(UserBook entity)
        {
            _unitOfWork.UserBooks.Delete(entity);
            _unitOfWork.SaveChanges();
        }

        public async Task DeleteAsync(UserBook entity)
        {
            await _unitOfWork.UserBooks.DeleteAsync(entity);
            await _unitOfWork.SaveChangesAsync();

        }

        public List<UserBook> GetAll(Expression<Func<UserBook, bool>> filter = null)
        {
            return filter == null ? _unitOfWork.UserBooks.GetAll() : _unitOfWork.UserBooks.GetAll(filter);
        }

        public async Task<List<UserBook>> GetAllAsync(Expression<Func<UserBook, bool>> filter = null)
        {
            return filter == null ? await _unitOfWork.UserBooks.GetAllAsync() : await _unitOfWork.UserBooks.GetAllAsync(filter);
        }

        public UserBook GetById(int id)
        {
            var UserBook = _unitOfWork.UserBooks.GetById(x => x.Id == id);
            return UserBook;
        }

        public async Task<UserBook> GetByIdAsync(int id)
        {
            var UserBook = await _unitOfWork.UserBooks.GetByIdAsync(x => x.Id == id);
            return UserBook;
        }


        public void Update(UserBook entity)
        {
            _unitOfWork.UserBooks.Update(entity);
            _unitOfWork.SaveChanges();

        }

        public async Task UpdateAsync(UserBook entity)
        {
            await _unitOfWork.UserBooks.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
