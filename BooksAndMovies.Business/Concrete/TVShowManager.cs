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
    public class TVShowManager : ITVShowService
    {
        private readonly ITVShowRepository _tVShowWatchListRepository;

        public TVShowManager(ITVShowRepository tVShowWatchListRepository)
        {
            _tVShowWatchListRepository = tVShowWatchListRepository;
        }
        public void Add(TVShow entity)
        {
            _tVShowWatchListRepository.Add(entity);
        }

        public async Task AddAsync(TVShow entity)
        {
            await _tVShowWatchListRepository.AddAsync(entity);
        }

        public void Delete(TVShow entity)
        {
            _tVShowWatchListRepository.Delete(entity);
        }

        public async Task DeleteAsync(TVShow entity)
        {
            await _tVShowWatchListRepository.DeleteAsync(entity);
        }

        public List<TVShow> GetAll(Expression<Func<TVShow, bool>> filter = null)
        {
            return filter == null ? _tVShowWatchListRepository.GetAll() : _tVShowWatchListRepository.GetAll(filter);
        }

        public async Task<List<TVShow>> GetAllAsync(Expression<Func<TVShow, bool>> filter = null)
        {
            return filter == null ? await _tVShowWatchListRepository.GetAllAsync() : await _tVShowWatchListRepository.GetAllAsync(filter);
        }

        public TVShow GetById(int id)
        {
            return _tVShowWatchListRepository.GetById(x => x.Id == id);
        }

        public async Task<TVShow> GetByIdAsync(int id)
        {
            return await _tVShowWatchListRepository.GetByIdAsync(x => x.Id == id);
        }

        public void Update(TVShow entity)
        {
            _tVShowWatchListRepository.Update(entity);
        }

        public async Task UpdateAsync(TVShow entity)
        {
            await _tVShowWatchListRepository.UpdateAsync(entity);
        }
    }
}
