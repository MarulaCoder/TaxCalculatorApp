using Microsoft.Extensions.DependencyInjection;
using TaxCalculator.Application.Services;
using AutoMapper;

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
                .AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies())
                .AddScoped<ITaxCalculatorService, TaxCalculatorService>();

            return services;
        }

        #endregion
    }
}
