using Microsoft.EntityFrameworkCore;
using NetDevPack.Data;
using NetDevPack.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDo.Domain.Interfaces.Base;


namespace ToDo.Infra.Data.Repository.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : Entity, IAggregateRoot
    {
        protected readonly Contexts.Context _context;
        protected readonly DbSet<TEntity> _dbSet;

        public BaseRepository(Contexts.Context context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<TEntity> SaveAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            return await Task.FromResult(entity);
        }

        public async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            _dbSet.Remove(entity);
            return await Task.FromResult(entity);
        }

        public async Task<TEntity> GetAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
