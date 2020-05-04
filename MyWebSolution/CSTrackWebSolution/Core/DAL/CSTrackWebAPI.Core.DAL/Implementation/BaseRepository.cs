using CSTrackWebAPI.Core.DAL.Abstraction.Interfaces;
using CSTrackWebAPI.Core.Model.Abstraction.Interfaces;
using CSTrackWebAPI.Model.Context.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CSTrackWebAPI.Core.DAL.Implementation
{
    public abstract class BaseRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TKey : IEquatable<TKey> where TEntity : class, ICoreEntity<TKey>
    {
        protected readonly ISQLContext _context;
        protected readonly DbSet<TEntity> DbSet;

        protected BaseRepository(ISQLContext context)
        {
            _context = context;
            DbSet = _context.GetCollection<TEntity>();
        }

        public virtual async Task AddAsync(TEntity obj)
        {
            await _context.AddCommand(DbSet.AddAsync(obj));
        }

        public virtual async Task<TEntity> GetByIdAsync(TKey id, params Expression<Func<TEntity, object>>[] includes)
        {
           // return await DbSet.FirstOrDefaultAsync(c => c.Id.Equals(id));

            return includes.Any() ?
                await includes
                  .Aggregate<Expression<Func<TEntity, object>>, IQueryable<TEntity>>
                    (DbSet, (current, expression) => current.Include(expression)).FirstOrDefaultAsync(c=>c.Id.Equals(id)) :
               await DbSet.FirstOrDefaultAsync(c=>c.Id.Equals(id));
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DbSet.AsQueryable();
        }

        public virtual Task UpdateAsync(TEntity obj)
        {
            return _context.AddCommand(Task.Run(()=> DbSet.Update(obj)));
        }

        public virtual Task RemoveAsync(TEntity obj) => _context.AddCommand(Task.Run(() => DbSet.Remove(obj)));

        public virtual Task RemoveRangeAsync(IEnumerable<TEntity> obj) => _context.AddCommand(Task.Run(() => DbSet.RemoveRange(obj)));

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
