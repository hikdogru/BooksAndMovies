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
        private readonly IUserTVShowRepository _userTVShowRepository;

        public UserTVShowManager(IUserTVShowRepository userTVShowRepository)
        {
            _userTVShowRepository = userTVShowRepository;
        }

        public void Add(UserTVShow entity)
        {
            _userTVShowRepository.Add(entity);
        }

        public async Task AddAsync(UserTVShow entity)
        {
            await _userTVShowRepository.AddAsync(entity);
        }

        public void Delete(UserTVShow entity)
        {
            _userTVShowRepository.Delete(entity);
        }

        public async Task DeleteAsync(UserTVShow entity)
        {
            await _userTVShowRepository.DeleteAsync(entity);
        }

        public List<UserTVShow> GetAll(Expression<Func<UserTVShow, bool>> filter = null)
        {
            return filter == null ? _userTVShowRepository.GetAll() : _userTVShowRepository.GetAll(filter);
        }

        public async Task<List<UserTVShow>> GetAllAsync(Expression<Func<UserTVShow, bool>> filter = null)
        {
            return filter == null ? await _userTVShowRepository.GetAllAsync() : await _userTVShowRepository.GetAllAsync(filter);
        }

        public UserTVShow GetById(int id)
        {
            var UserTVShow = _userTVShowRepository.GetById(x => x.Id == id);
            return UserTVShow;
        }

        public async Task<UserTVShow> GetByIdAsync(int id)
        {
            var UserTVShow = await _userTVShowRepository.GetByIdAsync(x => x.Id == id);
            return UserTVShow;
        }


        public void Update(UserTVShow entity)
        {
            _userTVShowRepository.Update(entity);
        }

        public async Task UpdateAsync(UserTVShow entity)
        {
            await _userTVShowRepository.UpdateAsync(entity);
        }
    }
}
