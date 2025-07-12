using disasterApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace disasterApi.Core.Interfaces.Infra.Database
{
    public interface IBaseRepository<TEntity> where TEntity : IBaseEntity
    {
        Task<TEntity> GetByIdAsync(Guid id);
        Task<bool> DoesExist(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> AddAsync(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity Remove(TEntity entity);
        Task SaveChangesAsync();
    }
    
}
