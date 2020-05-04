using CSTrackWebAPI.Core.Model.Abstraction.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CSTrackWebAPI.Core.DAL.Abstraction.Interfaces
{
    public interface IRepository<TEntity, TKey> : IDisposable where TKey : IEquatable<TKey> where TEntity : ICoreEntity<TKey>
    {
        Task AddAsync(TEntity obj);
        Task<TEntity> GetByIdAsync(TKey id, params Expression<Func<TEntity, object>>[] includes);
        IQueryable<TEntity> GetAll();
        Task UpdateAsync(TEntity obj);
        Task RemoveAsync(TEntity obj);
        Task RemoveRangeAsync(IEnumerable<TEntity> obj);
    }
}
