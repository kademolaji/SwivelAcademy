using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Swivel.Database
{
    public static class Configuration
    {
        public static IServiceCollection AddAppDbContext(
            this IServiceCollection services,
            string connectionString,
            int? commandTimeout = null,
            int? maxRetryCount = null,
            ServiceLifetime contextLifetime = ServiceLifetime.Scoped,
            ServiceLifetime optionsLifetime = ServiceLifetime.Scoped)
        {
            services
                .AddDbContext<AppDbContext>(optionsBuilder =>
                {
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
                },
                contextLifetime: contextLifetime,
                optionsLifetime: optionsLifetime);

            services.AddTransient<IAppDbContextFactory>((serviceProvider) => {
                return new AppDbContextFactory(
                    connectionString: connectionString,
                    commandTimeout: commandTimeout,
                    maxRetryCount: maxRetryCount);
            });

            return services;
        }
    }
}
