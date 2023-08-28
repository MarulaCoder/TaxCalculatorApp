using System.Net.Http.Headers;
using TaxCalculatorAppUI.Services;

namespace TaxCalculatorAppUI
{
    public static class DependencyInjection
    {
        #region Public Methods

        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {


            services
                .AddRepositories()
                .AddHttpClients();


            return services;
        }

        #endregion

        #region Private Methods

        private static IServiceCollection AddHttpClients(this IServiceCollection services) 
        {
            var baseAddress = @"https://localhost:7206/";
            services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });

            services.AddHttpClient("TaxCalculatorApi", client => 
            {
                client.BaseAddress = new Uri(baseAddress);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            });

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITaxService, TaxService>();

            return services;
        }

        #endregion
    }
}
