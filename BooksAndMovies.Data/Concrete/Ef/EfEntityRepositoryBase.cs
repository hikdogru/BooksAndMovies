using BooksAndMovies.Core.Data;
using BooksAndMovies.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BooksAndMovies.Data.Concrete.Ef
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        private readonly TContext _context;
        private DbSet<TEntity> Table { get; set; }

        public EfEntityRepositoryBase(TContext context)
        {
            _context = context;
            Table = _context.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            Table.Add(entity);
        }

        public async Task AddAsync(TEntity entity)
        {
            await Table.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            _context.Remove(entity);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _context.Remove(entity);
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ? Table.ToList() : Table.Where(filter).ToList();
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ? await Table.ToListAsync() : await Table.Where(filter).ToListAsync();
        }

        public TEntity GetById(Expression<Func<TEntity, bool>> filter)
        {
            return Table.SingleOrDefault(filter);
        }

        public async Task<TEntity> GetByIdAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await Table.SingleOrDefaultAsync(filter);
        }

        public void Update(TEntity entity)
        {
            var updatedEntity = _context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            var updatedEntity = _context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
        }
    }
}
