using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swivel.Database
{
    public interface IAppDbContextFactory
    {
        AppDbContext Create(
            string connectionString = null,
            int? commandTimeout = null,
            int? maxRetryCount = null);
    }

    public class AppDbContextFactory : IAppDbContextFactory
    {
        private readonly string _connectionString;
        private readonly int? _commandTimeout;
        private readonly int? _maxRetryCount;

        public AppDbContextFactory(
            string connectionString,
            int? commandTimeout = null,
            int? maxRetryCount = null,
            bool useAqSqlServerMigrationsSqlGenerator = false)
        {
            _connectionString = connectionString;
            _commandTimeout = commandTimeout;
            _maxRetryCount = maxRetryCount;
        }

        public AppDbContext Create(
            string connectionString = null,
            int? commandTimeout = null,
            int? maxRetryCount = null)
        {
            if (connectionString == null)
            {
                connectionString = _connectionString;
            }
            if (commandTimeout == null)
            {
                commandTimeout = _commandTimeout;
            }
            if (maxRetryCount == null)
            {
                maxRetryCount = _maxRetryCount;
            }
           

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder
                .UseNpgsql(connectionString, (sqlServerOptions) =>
                {
                    if (commandTimeout.HasValue)
                    {
                        sqlServerOptions.CommandTimeout(commandTimeout.Value);
                    }
                    if (maxRetryCount.HasValue)
                    {
                        sqlServerOptions.EnableRetryOnFailure(maxRetryCount.Value, TimeSpan.FromSeconds(10), null);
                    }
                });

            return new AppDbContext(options: optionsBuilder.Options);
        }
    }
}
