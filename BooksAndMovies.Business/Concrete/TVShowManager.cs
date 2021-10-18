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
        private readonly IUnitOfWork _unitOfWork;

        public TVShowManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        public void Add(TVShow entity)
        {
            if (IsTVShowExistInDatabase(entity: entity, databaseSaveType: entity.DatabaseSavingType) == false)
            {
                _unitOfWork.TVShows.Add(entity);
                _unitOfWork.SaveChanges();
            }
        }

        public async Task AddAsync(TVShow entity)
        {
            if (await IsTVShowExistInDatabaseAsync(entity: entity, databaseSaveType: entity.DatabaseSavingType) == false)
            {
                await _unitOfWork.TVShows.AddAsync(entity);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public void Delete(TVShow entity)
        {
            _unitOfWork.TVShows.Delete(entity);
            _unitOfWork.SaveChanges();
        }

        public async Task DeleteAsync(TVShow entity)
        {
            await _unitOfWork.TVShows.DeleteAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public List<TVShow> GetAll(Expression<Func<TVShow, bool>> filter = null)
        {
            return filter == null ? _unitOfWork.TVShows.GetAll() : _unitOfWork.TVShows.GetAll(filter);
        }

        public async Task<List<TVShow>> GetAllAsync(Expression<Func<TVShow, bool>> filter = null)
        {
            return filter == null ? await _unitOfWork.TVShows.GetAllAsync() : await _unitOfWork.TVShows.GetAllAsync(filter);
        }

        public TVShow GetById(int id)
        {
            return _unitOfWork.TVShows.GetById(x => x.Id == id);
        }

        public async Task<TVShow> GetByIdAsync(int id)
        {
            return await _unitOfWork.TVShows.GetByIdAsync(x => x.Id == id);
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
            _unitOfWork.TVShows.Update(entity);
            _unitOfWork.SaveChanges();
        }

        public async Task UpdateAsync(TVShow entity)
        {
            await _unitOfWork.TVShows.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
