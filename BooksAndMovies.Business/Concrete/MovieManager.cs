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
        private readonly IMovieRepository _movieRepository;

        public MovieManager(IMovieRepository tVShowWatchListRepository)
        {
            _movieRepository = tVShowWatchListRepository;
        }
        public void Add(Movie entity)
        {
            _movieRepository.Add(entity);
        }

        public async Task AddAsync(Movie entity)
        {
            await _movieRepository.AddAsync(entity);
        }

        public void Delete(Movie entity)
        {
            _movieRepository.Delete(entity);
        }

        public async Task DeleteAsync(Movie entity)
        {
            await _movieRepository.DeleteAsync(entity);
        }

        public List<Movie> GetAll(Expression<Func<Movie, bool>> filter = null)
        {
            return filter == null ? _movieRepository.GetAll() : _movieRepository.GetAll(filter);
        }

        
        public async Task<List<Movie>> GetAllAsync(Expression<Func<Movie, bool>> filter = null)
        {
            return filter == null ? await _movieRepository.GetAllAsync() : await _movieRepository.GetAllAsync(filter);
        }

        public Movie GetById(int id)
        {
            return _movieRepository.GetById(x => x.Id == id);
        }

        public async Task<Movie> GetByIdAsync(int id)
        {
            return await _movieRepository.GetByIdAsync(x => x.Id == id);
        }

        public void Update(Movie entity)
        {
            _movieRepository.Update(entity);
        }

        public async Task UpdateAsync(Movie entity)
        {
            await _movieRepository.UpdateAsync(entity);
        }
    }
}
