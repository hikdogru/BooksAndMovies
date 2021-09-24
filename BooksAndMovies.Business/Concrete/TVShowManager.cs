using BooksAndMovies.Business.Abstract;
using BooksAndMovies.Data.Abstract;
using BooksAndMovies.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public void Add(TVShowWatchList entity)
        {
            _tVShowWatchListRepository.Add(entity);
        }

        public async Task AddAsync(TVShowWatchList entity)
        {
            await _tVShowWatchListRepository.AddAsync(entity);
        }

        public void Delete(TVShowWatchList entity)
        {
            _tVShowWatchListRepository.Delete(entity);
        }

        public async Task DeleteAsync(TVShowWatchList entity)
        {
            await _tVShowWatchListRepository.DeleteAsync(entity);
        }

        public List<TVShowWatchList> GetAll()
        {
            return _tVShowWatchListRepository.GetAll();
        }

        public async Task<List<TVShowWatchList>> GetAllAsync()
        {
            return await _tVShowWatchListRepository.GetAllAsync();
        }

        public TVShowWatchList GetById(int id)
        {
            return _tVShowWatchListRepository.GetById(x => x.Id == id);
        }

        public async Task<TVShowWatchList> GetByIdAsync(int id)
        {
            return await _tVShowWatchListRepository.GetByIdAsync(x => x.Id == id);
        }

        public void Update(TVShowWatchList entity)
        {
            _tVShowWatchListRepository.Update(entity);
        }

        public async Task UpdateAsync(TVShowWatchList entity)
        {
            await _tVShowWatchListRepository.UpdateAsync(entity);
        }
    }
}
