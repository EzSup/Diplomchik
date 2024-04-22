using Azure.Core;
using Newtonsoft.Json;
using Restaurant.Client.Contracts.Customers;
using Restaurant.Client.Contracts.Tables;
using Restaurant.Client.Services.Interfaces;

namespace Restaurant.Client.Services
{
    public class TablesService : ITablesService
    {
        private readonly HttpClient _httpClient;

        public TablesService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("API");
        }

        public async Task<ICollection<TableResponse>> GetTablesOfTime(DateTime dateTime)
        {
            var isoDateTimeString = dateTime.ToString("yyyy-MM-ddTHH:mm:ssZ");
            var response = await _httpClient.GetAsync($"api/Tables/GetTablesOfTime/{isoDateTimeString}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ICollection<TableResponse>>(responseBody);
            return result;
        }

        public async Task<Guid> Reserve(ReserveRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/Tables/ReserveTable", request);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Guid>(responseBody);
            return result;
        }
    }
}
