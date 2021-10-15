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
    public class UserMovieManager : IUserMovieService
    {
        private readonly IUserMovieRepository _movieRepository;

        public UserMovieManager(IUserMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public void Add(UserMovie entity)
        {
            _movieRepository.Add(entity);
        }

        public async Task AddAsync(UserMovie entity)
        {
            await _movieRepository.AddAsync(entity);
        }

        public void Delete(UserMovie entity)
        {
            _movieRepository.Delete(entity);
        }

        public async Task DeleteAsync(UserMovie entity)
        {
            await _movieRepository.DeleteAsync(entity);
        }

        public List<UserMovie> GetAll(Expression<Func<UserMovie, bool>> filter = null)
        {
            return filter == null ? _movieRepository.GetAll() : _movieRepository.GetAll(filter);
        }

        public async Task<List<UserMovie>> GetAllAsync(Expression<Func<UserMovie, bool>> filter = null)
        {
            return filter == null ? await _movieRepository.GetAllAsync() : await _movieRepository.GetAllAsync(filter);
        }

        public UserMovie GetById(int id)
        {
            var UserMovie = _movieRepository.GetById(x => x.Id == id);
            return UserMovie;
        }

        public async Task<UserMovie> GetByIdAsync(int id)
        {
            var UserMovie = await _movieRepository.GetByIdAsync(x => x.Id == id);
            return UserMovie;
        }


        public void Update(UserMovie entity)
        {
            _movieRepository.Update(entity);
        }

        public async Task UpdateAsync(UserMovie entity)
        {
            await _movieRepository.UpdateAsync(entity);
        }
    }
}
