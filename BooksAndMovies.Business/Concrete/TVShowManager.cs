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
        private readonly ITVShowRepository _tvShowRepository;

        public TVShowManager(ITVShowRepository tvShowRepository)
        {
            _tvShowRepository = tvShowRepository;
        }
        public void Add(TVShow entity)
        {
            if (IsTVShowExistInDatabase(entity: entity, databaseSaveType: entity.DatabaseSavingType) == false)
            {
                _tvShowRepository.Add(entity);
            }
        }

        public async Task AddAsync(TVShow entity)
        {
            if (await IsTVShowExistInDatabaseAsync(entity: entity, databaseSaveType: entity.DatabaseSavingType) == false)
            {
                await _tvShowRepository.AddAsync(entity);
            }
        }

        public void Delete(TVShow entity)
        {
            _tvShowRepository.Delete(entity);
        }

        public async Task DeleteAsync(TVShow entity)
        {
            await _tvShowRepository.DeleteAsync(entity);
        }

        public List<TVShow> GetAll(Expression<Func<TVShow, bool>> filter = null)
        {
            return filter == null ? _tvShowRepository.GetAll() : _tvShowRepository.GetAll(filter);
        }

        public async Task<List<TVShow>> GetAllAsync(Expression<Func<TVShow, bool>> filter = null)
        {
            return filter == null ? await _tvShowRepository.GetAllAsync() : await _tvShowRepository.GetAllAsync(filter);
        }

        public TVShow GetById(int id)
        {
            return _tvShowRepository.GetById(x => x.Id == id);
        }

        public async Task<TVShow> GetByIdAsync(int id)
        {
            return await _tvShowRepository.GetByIdAsync(x => x.Id == id);
        }

        public bool IsTVShowExistInDatabase(TVShow entity, int databaseSaveType)
        {
            var isExist = GetAll(x => x.BackdropPath == entity.BackdropPath && x.DatabaseSavingType == databaseSaveType);
            return isExist.Count == 0 ? false : true;
        }

        public async Task<bool> IsTVShowExistInDatabaseAsync(TVShow entity, int databaseSaveType)
        {
            var isExist = await GetAllAsync(x => x.BackdropPath == entity.BackdropPath && x.DatabaseSavingType == databaseSaveType);
            return isExist.Count == 0 ? false : true;
        }

        public void Update(TVShow entity)
        {
            _tvShowRepository.Update(entity);
        }

        public async Task UpdateAsync(TVShow entity)
        {
            await _tvShowRepository.UpdateAsync(entity);
        }
    }
}
