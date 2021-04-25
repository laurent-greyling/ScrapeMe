using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;

namespace Scraper.Utilities
{
    [ExcludeFromCodeCoverage]
    public class RestClient : IRestClient
    {
        private readonly HttpClient _httpClient;

        public RestClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> RetrieveAsync(string apiPath)
        {
            Ensure.ArgumentNotNullOrEmptyString(apiPath, nameof(apiPath));

            var response = await _httpClient.GetAsync(apiPath);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
