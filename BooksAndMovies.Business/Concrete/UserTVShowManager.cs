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
    public class UserTVShowManager : IUserTVShowService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserTVShowManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public void Add(UserTVShow entity)
        {
            _unitOfWork.UserTVShows.Add(entity);
            _unitOfWork.SaveChanges();
        }

        public async Task AddAsync(UserTVShow entity)
        {
            await _unitOfWork.UserTVShows.AddAsync(entity);
            await _unitOfWork.SaveChangesAsync();

        }

        public void Delete(UserTVShow entity)
        {
            _unitOfWork.UserTVShows.Delete(entity);
            _unitOfWork.SaveChanges();

        }

        public async Task DeleteAsync(UserTVShow entity)
        {
            await _unitOfWork.UserTVShows.DeleteAsync(entity);
            await _unitOfWork.SaveChangesAsync();

        }

        public List<UserTVShow> GetAll(Expression<Func<UserTVShow, bool>> filter = null)
        {
            return filter == null ? _unitOfWork.UserTVShows.GetAll() : _unitOfWork.UserTVShows.GetAll(filter);
        }

        public async Task<List<UserTVShow>> GetAllAsync(Expression<Func<UserTVShow, bool>> filter = null)
        {
            return filter == null ? await _unitOfWork.UserTVShows.GetAllAsync() : await _unitOfWork.UserTVShows.GetAllAsync(filter);
        }

        public UserTVShow GetById(int id)
        {
            var UserTVShow = _unitOfWork.UserTVShows.GetById(x => x.Id == id);
            return UserTVShow;
        }

        public async Task<UserTVShow> GetByIdAsync(int id)
        {
            var UserTVShow = await _unitOfWork.UserTVShows.GetByIdAsync(x => x.Id == id);
            return UserTVShow;
        }


        public void Update(UserTVShow entity)
        {
            _unitOfWork.UserTVShows.Update(entity);
            _unitOfWork.SaveChanges();

        }

        public async Task UpdateAsync(UserTVShow entity)
        {
            await _unitOfWork.UserTVShows.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
