using BooksAndMovies.Business.Abstract;
using BooksAndMovies.Data.Abstract;
using BooksAndMovies.Entity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BooksAndMovies.Business.Concrete
{
    public class BookManager : IBookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BookManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        
        public void Add(Book entity)
        {
            if(IsBookExistInDatabase(entity : entity, databaseSaveType : entity.DatabaseSavingType ) == false)
            {
                _unitOfWork.Books.Add(entity);
                _unitOfWork.SaveChanges();
            }
        }

        public async Task AddAsync(Book entity)
        {
            if (await IsBookExistInDatabaseAsync(entity: entity, databaseSaveType: entity.DatabaseSavingType) == false)
            {
                await _unitOfWork.Books.AddAsync(entity);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public void Delete(Book entity)
        {
            _unitOfWork.Books.Delete(entity);
            _unitOfWork.SaveChanges();
        }

        public async Task DeleteAsync(Book entity)
        {
            await _unitOfWork.Books.DeleteAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }

        public List<Book> GetAll(Expression<Func<Book, bool>> filter = null)
        {
            return filter == null ? _unitOfWork.Books.GetAll() : _unitOfWork.Books.GetAll(filter);
        }

        public async Task<List<Book>> GetAllAsync(Expression<Func<Book, bool>> filter = null)
        {
            return filter == null ? await _unitOfWork.Books.GetAllAsync() : await _unitOfWork.Books.GetAllAsync(filter);
        }

        public Book GetById(int id)
        {
            return _unitOfWork.Books.GetById(x => x.Id == id);
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _unitOfWork.Books.GetByIdAsync(x => x.Id == id);
        }

        public bool IsBookExistInDatabase(Book entity, int databaseSaveType)
        {
            var isExist = GetAll(x => x.Thumbnail == entity.Thumbnail && x.DatabaseSavingType == databaseSaveType);
            return isExist.Count == 0 ? false : true;
        }

        public async Task<bool> IsBookExistInDatabaseAsync(Book entity, int databaseSaveType)
        {
            var isExist = await GetAllAsync(x => x.Thumbnail == entity.Thumbnail && x.DatabaseSavingType == databaseSaveType);
            return isExist.Count == 0 ? false : true;
        }

        public void Update(Book entity)
        {
            _unitOfWork.Books.Update(entity);
            _unitOfWork.SaveChanges();
        }

        public async Task UpdateAsync(Book entity)
        {
            await _unitOfWork.Books.UpdateAsync(entity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
