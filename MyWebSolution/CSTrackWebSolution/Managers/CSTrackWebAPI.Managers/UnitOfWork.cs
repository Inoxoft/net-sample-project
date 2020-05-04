using AutoMapper;
using CSTrackWebAPI.Common.StoredProcedures;
using CSTrackWebAPI.DAL.Abstraction.Interfaces;
using CSTrackWebAPI.DAL.Repositories;
using CSTrackWebAPI.Managers.Abstraction;
using CSTrackWebAPI.Model.Context.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CSTrackWebAPI.Managers
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISQLContext _context;

        private readonly IMapper _mapper;

        private IPlayerRepository _patientRepository;

        public UnitOfWork(ISQLContext context,
            IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        #region Repositories

        public IPlayerRepository PlayerRepository
        {
            get
            {
                _patientRepository = _patientRepository ?? new PlayerRepository(_context);
                return _patientRepository;
            }
        }
        
        #endregion

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.BeginTransactionAsync();
        }

        public async Task<List<T>> ExecuteStoredProcedureAsync<T>(StoredProcedureSettings settings) where T : class, new()
        {
            return await _context.ExecuteStoredProcedureAsync<T>(settings);
        }

        public async Task ExecuteSqlCommandAsync(RawSqlString sql)
        {
            await _context.ExecuteSqlCommandAsync(sql);
        }

        public async Task<bool> CommitAsync()
        {
            var result = await _context.SaveChangesAsync();
            return await Task.FromResult(result > 0);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
