using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Application.Services;
using TaxCalculator.Domain.Core.Repositories;

namespace TaxCalculator.Application
{
    public static class DependencyInjection
    {
        #region Public Methods

        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddRepositories();

            return services;
        }

        #endregion

        #region Private Methods

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services
                .AddScoped<ITaxCalculatorService, TaxCalculatorService>();

            return services;
        }

        #endregion
    }
}
