using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using TaxCalculatorApp.Models.Core;
using TaxCalculatorApp.Models.Requests;

namespace TaxCalculatorApp.Data
{
    public class TaxCalculatorData : ITaxCalculatorData
    {
        #region Fields

        //private HttpClient _httpClient;
        private readonly IHttpClientFactory _clientFactory;

        #endregion

        #region Constructors

        public TaxCalculatorData(IHttpClientFactory clientFactory) 
        {
            //_httpClient = httpClient;
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
        }

        #endregion

        #region Public Methods

        public async Task<TaxCalculation> CalculateTax(TaxCalculateRequest calculateRequest, CancellationToken cancellationToken)
        {
            var result = new TaxCalculation();
            var requestUri = $"api/TaxCalculator/calculate";

            var taxRequest = JsonSerializer.Serialize(calculateRequest);
            var content = new StringContent(taxRequest, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            request.Headers.Add("Accept", "application/vnd.github.v3+json");
            request.Content = content;

            var _httpClient = _clientFactory.CreateClient("TaxCalculatorApi");

            var response = await _httpClient.SendAsync(request, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                result = JsonSerializer.Deserialize<TaxCalculation>(stringResponse,
                    new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }

            return result;
        }

        public async Task<TaxInformation> GetTaxInformation(CancellationToken cancellationToken)
        { 
            var result = new TaxInformation();


            return result;
        }

        #endregion
    }
}
