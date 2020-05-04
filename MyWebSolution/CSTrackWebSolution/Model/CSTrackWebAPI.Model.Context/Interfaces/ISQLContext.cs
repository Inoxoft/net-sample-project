using CSTrackWebAPI.Common.StoredProcedures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSTrackWebAPI.Model.Context.Interfaces
{
    public interface ISQLContext
    {
        DbSet<T> GetCollection<T>() where T:class;
        Task AddCommand(Task func); 
        Task<int> SaveChangesAsync();
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task<List<T>> ExecuteStoredProcedureAsync<T>(StoredProcedureSettings settings) where T : class, new();
        Task ExecuteSqlCommandAsync(RawSqlString sql);
        void Dispose();
    }
}
