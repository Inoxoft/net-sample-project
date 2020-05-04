using CSTrackWebAPI.Common.StoredProcedures;
using CSTrackWebAPI.DAL.Abstraction.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSTrackWebAPI.Managers.Abstraction
{
    public interface IUnitOfWork : IDisposable
    {
        #region Repositories   
        IPlayerRepository PlayerRepository { get; }
        #endregion


        Task<IDbContextTransaction> BeginTransactionAsync();
        Task<List<T>> ExecuteStoredProcedureAsync<T>(StoredProcedureSettings settings) where T : class, new();
        Task ExecuteSqlCommandAsync(RawSqlString sql);
        Task<bool> CommitAsync();
    }
}
