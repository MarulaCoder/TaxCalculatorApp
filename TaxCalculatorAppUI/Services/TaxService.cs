using System.Text.Json;
using System.Text;
using System.Net.Http.Json;
using TaxCalculatorAppUI.Models.Core;
using TaxCalculatorAppUI.Models.Requests;
using static System.Net.WebRequestMethods;
using System.Net.Http;

namespace TaxCalculatorAppUI.Services
{
    public class TaxService : ITaxService
    {
        #region Fields

        private readonly string _url = $"api/taxcalculator";
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _options;
        private readonly IHttpClientFactory _clientFactory;

        #endregion

        #region Constructors

        public TaxService(HttpClient client, IHttpClientFactory clientFactory)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true }; //new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }
        }

        #endregion

        #region Public Methods

        public async Task<TaxCalculation> CalculateTax(TaxCalculateRequest calculateRequest, CancellationToken cancellationToken)
        {
            var result = new TaxCalculation();
            var requestUri = $"{_url}/calculate";

            var taxRequest = JsonSerializer.Serialize(calculateRequest);
            var content = new StringContent(taxRequest, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            request.Headers.Add("Accept", "application/vnd.github.v3+json");
            request.Content = content;

            var response = await _client.SendAsync(request, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                result = JsonSerializer.Deserialize<TaxCalculation>(stringResponse, _options);
            }

            return result;
        }

        public async Task<TaxInformation> GetTaxInformation(CancellationToken cancellationToken)
        {
            var result = new TaxInformation();
            var requestUri = $"{_url}/information";

            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            request.Headers.Add("Accept", "application/json");

            //var response = await _client.SendAsync(request, cancellationToken);
            //result = await _client.GetFromJsonAsync<TaxInformation>(requestUri);

            var _client2 = _clientFactory.CreateClient("TaxCalculatorApi");
            result = await _client2.GetFromJsonAsync<TaxInformation>(requestUri);

            //if (response.IsSuccessStatusCode)
            //{
            //    var stringResponse = await response.Content.ReadAsStringAsync();

            //    result = JsonSerializer.Deserialize<TaxInformation>(stringResponse, _options);
            //}

            return result;
        }

        #endregion
    }
}
