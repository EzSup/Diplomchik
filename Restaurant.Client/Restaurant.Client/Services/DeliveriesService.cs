using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using Restaurant.Client.Contracts.Blogs;
using Restaurant.Client.Contracts.Deliveries;
using Restaurant.Client.Contracts.Tables;
using Restaurant.Client.Services.Interfaces;

namespace Restaurant.Client.Services
{
    public class DeliveriesService : IDeliveriesService
    {
        private HttpClient _httpClient;

        public DeliveriesService(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("API");
        }        

        public async Task<Guid> RegisterDelivery(DeliveryAddRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync($"/api/Deliveries/Post", request);
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Guid>(responseBody);
            return result;
        }

        public async Task<ICollection<DeliveryResponse>> GetDeliveriesByCustomer(int pageIndex, int pageSize, Guid customerId)
        {
            var response = await _httpClient.GetAsync($"/api/Deliveries/GetByCustomerId?pageIndex={pageIndex}&pageSize={pageSize}&customerId={customerId}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ICollection<DeliveryResponse>>(responseBody);
            return result;
        }
    }
}
