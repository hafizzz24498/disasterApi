using disasterApi.Core.Interfaces.Infra.Database;
using disasterApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace disasterApi.Infra.Database.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DataContext _context;
        public BaseRepository(DataContext context)
        {
            this._context = context;
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        public async Task<bool> DoesExist(Expression<Func<TEntity, bool>> predicate)
        {
            return await _context.Set<TEntity>().AnyAsync(predicate);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            entity.Id = Guid.NewGuid();
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = entity.CreatedAt;
            entity.IsDeleted = false;

            await _context.Set<TEntity>().AddAsync(entity);

            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;

            return entity;
        }

        public TEntity Remove(TEntity entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            entity.IsDeleted = true;

            return entity;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
