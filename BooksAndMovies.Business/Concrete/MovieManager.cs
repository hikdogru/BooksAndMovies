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
    public class MovieManager : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieManager(IMovieRepository tVShowWatchListRepository)
        {
            _movieRepository = tVShowWatchListRepository;
        }
        public void Add(MovieWatchList entity)
        {
            _movieRepository.Add(entity);
        }

        public async Task AddAsync(MovieWatchList entity)
        {
            await _movieRepository.AddAsync(entity);
        }

        public void Delete(MovieWatchList entity)
        {
            _movieRepository.Delete(entity);
        }

        public async Task DeleteAsync(MovieWatchList entity)
        {
            await _movieRepository.DeleteAsync(entity);
        }

        public List<MovieWatchList> GetAll()
        {
            return _movieRepository.GetAll();
        }

        public async Task<List<MovieWatchList>> GetAllAsync()
        {
            return await _movieRepository.GetAllAsync();
        }

        public MovieWatchList GetById(int id)
        {
            return _movieRepository.GetById(x => x.Id == id);
        }

        public async Task<MovieWatchList> GetByIdAsync(int id)
        {
            return await _movieRepository.GetByIdAsync(x => x.Id == id);
        }

        public void Update(MovieWatchList entity)
        {
            _movieRepository.Update(entity);
        }

        public async Task UpdateAsync(MovieWatchList entity)
        {
            await _movieRepository.UpdateAsync(entity);
        }
    }
}
