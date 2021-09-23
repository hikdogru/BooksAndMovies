using BooksAndMovies.Core.Data.Abstract;
using BooksAndMovies.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndMovies.Core.Data.Concrete.Ef
{
    public class EfEntityRepositoryBase<TEntity, T_context> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where T_context : DbContext, new()
    {
        private readonly T_context _context;

        public EfEntityRepositoryBase(T_context context)
        {
            _context = context;
        }

        public void Add(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ? _context.Set<TEntity>().ToList() : _context.Set<TEntity>().Where(filter).ToList();
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ? await _context.Set<TEntity>().ToListAsync() : await _context.Set<TEntity>().Where(filter).ToListAsync();
        }

        public TEntity GetById(Expression<Func<TEntity, bool>> filter)
        {
            return _context.Set<TEntity>().SingleOrDefault(filter);
        }

        public async Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _context.Set<TEntity>().SingleOrDefaultAsync(filter);
        }

        public void Update(TEntity entity)
        {
            var updatedEntity = _context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            var updatedEntity = _context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
