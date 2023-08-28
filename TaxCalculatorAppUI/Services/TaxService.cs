using System.Text.Json;
using System.Text;
using TaxCalculatorAppUI.Models.Core;
using TaxCalculatorAppUI.Models.Requests;

namespace TaxCalculatorAppUI.Services
{
    public class TaxService : ITaxService
    {
        #region Fields

        private readonly HttpClient _httpClient;
        private readonly string _url = $"api/taxcalculator";
        private readonly JsonSerializerOptions _options;
        private readonly IHttpClientFactory _clientFactory;

        #endregion

        #region Constructors

        public TaxService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory ?? throw new ArgumentNullException(nameof(clientFactory));
            _httpClient = _clientFactory.CreateClient("TaxCalculatorApi");

            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        #endregion

        #region Public Methods

        public async Task<TaxCalculation> CalculateTax(TaxCalculateRequest calculateRequest)
        {
            var result = new TaxCalculation();
            var requestUri = $"{_url}/calculate";

            var content = new StringContent(JsonSerializer.Serialize(calculateRequest, _options), Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            request.Content = content;

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                result = JsonSerializer.Deserialize<TaxCalculation>(stringResponse, _options);
            }

            return result;
        }

        public async Task<TaxInformation> GetTaxInformation()
        {
            var requestUri = $"{_url}/information";

            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<TaxInformation>(stringResponse, _options);
            }

            return null;
        }

        public async Task<IEnumerable<TaxHistory>> GetCalculatedTaxHistory()
        {
            var requestUri = $"{_url}/calculated";

            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<IEnumerable<TaxHistory>>(stringResponse, _options);
            }

            return Enumerable.Empty<TaxHistory>();
        }

        public async Task<IEnumerable<string>> GetPostalCodes()
        {
            var requestUri = $"{_url}/codes";

            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<IEnumerable<string>>(stringResponse, _options);
            }

            return Enumerable.Empty<string>();
        }

        public async Task DeleteTaxHistoryItem(int id)
        {
            var requestUri = $"{_url}/calculated/delete/{id}";

            var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);

            await _httpClient.SendAsync(request);
        }

        public async Task UpdateProgressiveTaxItem(ProgressiveTax progressiveTax)
        {
            var requestUri = $"{_url}/tax/progressive/update/{progressiveTax.Id}";

            var content = new StringContent(JsonSerializer.Serialize(progressiveTax, _options), Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Put, requestUri);
            request.Content = content;

            await _httpClient.SendAsync(request);
        }

        public async Task DeleteProgressiveTaxItem(int id)
        {
            var requestUri = $"{_url}/tax/progressive/delete/{id}";

            var request = new HttpRequestMessage(HttpMethod.Delete, requestUri);

            await _httpClient.SendAsync(request);
        }

        #endregion
    }
}
