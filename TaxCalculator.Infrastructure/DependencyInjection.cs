﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaxCalculator.Domain.Core.Repositories;
using TaxCalculator.Infrastructure.Context;
using TaxCalculator.Infrastructure.Repositories;

namespace TaxCalculator.Infrastructure
{
    public static class DependencyInjection
    {
        #region Public Methods

        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext(configuration)
                .AddRepositories();

            return services;
        }

        #endregion

        #region Private Methods

        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("TaxCalculatorDB");

            services.AddDbContext<AppDbContext>(options =>
               options.UseSqlServer(connectionString,
                   builder => builder.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services
                .AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork))
                .AddScoped(typeof(IRepository), typeof(Repository))
                .AddScoped<ITaxRepository, TaxRepository>();

            return services;
        }

        #endregion
    }
}
