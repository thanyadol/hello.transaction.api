using System;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using hello.transaction.core.Models;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace hello.transaction.core.Repositories
{
    /*
     * 
     *
     */

    public interface IGenericRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    {
        Task<IEnumerable<TEntity>> ListAsync();

        Task<TEntity> GetAsync(TKey id);
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity, TKey id);
        Task<IEnumerable<TEntity>> CreateRangeAsync(IEnumerable<TEntity> entities);
    }

    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
     where TEntity : class, IEntity<TKey>
    {
        private readonly NorthwindContext _dbContext;

        public GenericRepository(NorthwindContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IEnumerable<TEntity>> ListAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> CreateRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbContext.Set<TEntity>().AddRangeAsync(entities);
            _dbContext.SaveChanges();

            return entities;
        }


        public async Task<TEntity> CreateAsync(TEntity entity)
        {

            var createdEntity = await _dbContext.Set<TEntity>().AddAsync(entity);
            _dbContext.SaveChanges();

            return createdEntity.Entity;
        }

        public async Task<TEntity> DeleteAsync(TKey id)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);
            _dbContext.Set<TEntity>().Remove(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity, TKey id)
        {

            var updatedEntity = await _dbContext.Set<TEntity>().FindAsync(id);
            _dbContext.Set<TEntity>().Update(entity);

            _dbContext.SaveChanges();
            return entity;
        }

        public async Task<TEntity> GetAsync(TKey id)
        {
            var entity = await _dbContext.Set<TEntity>().FindAsync(id);
            return entity;
        }

    }
}