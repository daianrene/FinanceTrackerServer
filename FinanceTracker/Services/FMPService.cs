using FinanceTracker.Dto.Stock;
using FinanceTracker.Mapper;
using FinanceTracker.Models;
using Newtonsoft.Json;

namespace FinanceTracker.Services
{
    public class FMPService : IFMPService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public FMPService(IConfiguration configuration, HttpClient httpClient)
        {
            _configuration = configuration;
            _httpClient = httpClient;
        }

        public async Task<Stock> FindStockBySymbol(string symbol)
        {
            try
            {
                string apiUrl = $"https://financialmodelingprep.com/api/v3/profile/{symbol}?apikey={_configuration["FMPApiKey"]}";

                var result = await _httpClient.GetAsync(apiUrl);

                if (result.IsSuccessStatusCode)
                {
                    var content = await result.Content.ReadAsStringAsync();
                    var tasks = JsonConvert.DeserializeObject<FMPStock[]>(content);
                    var stock = tasks[0];

                    if (stock != null) return stock.ToStockFromFMP();
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            };
        }
    }
}
