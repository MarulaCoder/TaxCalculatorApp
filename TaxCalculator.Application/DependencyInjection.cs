using Microsoft.Extensions.DependencyInjection;
using TaxCalculator.Application.Services;

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
