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
    public class MovieManager : IMovieService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MovieManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        public void Add(Movie entity)
        {
            if (IsMovieExistInDatabase(entity: entity, databaseSaveType: entity.DatabaseSavingType) == false)
            {
                _unitOfWork.Movies.Add(entity);
                _unitOfWork.SaveChanges();
            }
        }

        public async Task AddAsync(Movie entity)
        {
            if (await IsMovieExistInDatabaseAsync(entity: entity, databaseSaveType: entity.DatabaseSavingType) == false)
            {
                await _unitOfWork.Movies.AddAsync(entity);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public void Delete(Movie entity)
        {
            _unitOfWork.Movies.Delete(entity);
            _unitOfWork.SaveChanges();
        }

        public async Task DeleteAsync(Movie entity)
        {

            await _unitOfWork.Movies.DeleteAsync(entity);
            await _unitOfWork.SaveChangesAsync();

        }

        public List<Movie> GetAll(Expression<Func<Movie, bool>> filter = null)
        {
            return filter == null ? _unitOfWork.Movies.GetAll() : _unitOfWork.Movies.GetAll(filter);
        }


        public async Task<List<Movie>> GetAllAsync(Expression<Func<Movie, bool>> filter = null)
        {
            return filter == null ? await _unitOfWork.Movies.GetAllAsync() : await _unitOfWork.Movies.GetAllAsync(filter);
        }

        public Movie GetById(int id)
        {
            return _unitOfWork.Movies.GetById(x => x.Id == id);
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            return await _unitOfWork.Movies.GetByIdAsync(x => x.Id == id);
        }

        public bool IsMovieExistInDatabase(Movie entity, int databaseSaveType)
        {
            var isExist = GetAll(x => x.BackdropPath == entity.BackdropPath && x.DatabaseSavingType == databaseSaveType);
            return isExist.Count == 0 ? false : true;
        }

        public async Task<bool> IsMovieExistInDatabaseAsync(Movie entity, int databaseSaveType)
        {
            var isExist = await GetAllAsync(x => x.BackdropPath == entity.BackdropPath && x.DatabaseSavingType == databaseSaveType);
            return isExist.Count == 0 ? false : true;
        }

        public void Update(Movie entity)
        {
            bool isMovieExist = IsMovieExistInDatabase(entity: entity, databaseSaveType: entity.DatabaseSavingType);
            if (isMovieExist == false)
            {
                _unitOfWork.Movies.Update(entity);
                _unitOfWork.SaveChanges();
            }
        }

        public async Task UpdateAsync(Movie entity)
        {
            bool isMovieExist = await IsMovieExistInDatabaseAsync(entity: entity, databaseSaveType: entity.DatabaseSavingType);
            if (isMovieExist == false)
            {
                await _unitOfWork.Movies.UpdateAsync(entity);
                await _unitOfWork.SaveChangesAsync();
            }
        }
    }
}
