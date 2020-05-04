using CSTrackWebAPI.Common.StoredProcedures;
using CSTrackWebAPI.Model.Context.Interfaces;
using CSTrackWebAPI.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using StoredProcedureEFCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CSTrackWebAPI.Model.Context
{
    public class SQLContext : DbContext, ISQLContext
    {
        private readonly List<Task> _commands;
        private ILogger _logger;

        public SQLContext(DbContextOptions<SQLContext> options, ILogger<SQLContext> logger)
            : base(options)
        {
            _commands = new List<Task>();
            _logger = logger;
        }

        public DbSet<Player> Players { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public async Task<int> SaveChangesAsync()
        {
            using (var transaction = await this.BeginTransactionAsync())
            {
                try
                {
                    await Task.WhenAll(_commands);

                    _commands.Clear();

                    var result = await this.SaveChangesAsync(System.Threading.CancellationToken.None);

                    transaction.Commit();

                    return result;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    DetachAllEntities();

                    _logger.LogError(ex, "SQLContext.SaveChangesAsync");

                    if (ex.InnerException != null)
                    {
                        _logger.LogError(ex.InnerException, "SQLContext.SaveChangesAsync InnerException");
                    }

                    throw;
                }
            }
        }

        private void DetachAllEntities()
        {
            var changedEntriesCopy = this.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added ||
                            e.State == EntityState.Modified ||
                            e.State == EntityState.Deleted)
                .ToList();

            foreach (var entry in changedEntriesCopy)
                entry.State = EntityState.Detached;
        }

        public DbSet<T> GetCollection<T>() where T : class
        {
            return this.Set<T>();
        }

        public Task AddCommand(Task func)
        {
            _commands.Add(func);
            return Task.CompletedTask;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await this.Database.BeginTransactionAsync(System.Data.IsolationLevel.ReadCommitted);
        }

        public async Task<List<T>> ExecuteStoredProcedureAsync<T>(StoredProcedureSettings settings) where T : class, new()
        {
            List<T> result = new List<T>();

            var builder = this.LoadStoredProc(settings.Name);

            foreach (var param in settings.Parameters)
            {
                builder.AddParam(param.Name, param.Value);
            }

            await builder.ExecAsync(async r => result = await r.ToListAsync<T>());

            return await Task.FromResult(result);
        }

        public async Task ExecuteSqlCommandAsync(RawSqlString sql)
        {
            await this.Database.ExecuteSqlCommandAsync(sql);
        }



        public override void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
