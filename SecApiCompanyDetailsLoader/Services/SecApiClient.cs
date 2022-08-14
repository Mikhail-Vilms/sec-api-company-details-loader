using SecApiCompanyDetailsLoader.IServices;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SecApiCompanyDetailsLoader.Services
{
    public class SecApiClient : ISecApiClient
    {
        private readonly string _companyTickersUrl = "https://www.sec.gov/files/company_tickers.json";
        private readonly HttpClient _httpClient;

        public SecApiClient()
        {
            _httpClient = new HttpClient();

            _httpClient
                .DefaultRequestHeaders
                .UserAgent
                .Add(new ProductInfoHeaderValue("ScraperBot", "1.0"));
            _httpClient
                .DefaultRequestHeaders
                .UserAgent
                .Add(new ProductInfoHeaderValue("(+http://www.example.com/ScraperBot.html)"));
        }

        public async Task<Stream> GetCompanyTickersFileStream()
        {
            return await _httpClient.GetStreamAsync(_companyTickersUrl);
        }
    }
}
