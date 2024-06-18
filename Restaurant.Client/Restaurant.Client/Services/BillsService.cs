using Microsoft.JSInterop;
using Newtonsoft.Json;
using Restaurant.Client.Contracts.Bill;
using Restaurant.Client.Contracts.Carts;
using Restaurant.Client.Contracts.Enums;
using Restaurant.Client.Services.Interfaces;
using Blazored.Toast;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using System.Drawing.Printing;

namespace Restaurant.Client.Services
{
    public class BillsService : IBillsService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly IJSRuntime _jsruntime;
        private readonly IToastService _toastService;
        private readonly NavigationManager _navManager;

        public BillsService(IHttpClientFactory factory, ILocalStorageService localStorage, IJSRuntime runtime, IToastService toastService, NavigationManager navManager)
        {
            _httpClient = factory.CreateClient("API");
            _localStorage = localStorage;
            _jsruntime = runtime;
            _toastService = toastService;
            _navManager = navManager;
        }

        public async Task AddBill(Guid ReservationOrDeliveryId, OrderType Type)
        {
            var customerId = Guid.Parse(await _localStorage.GetItem("customerId"));
            var request = new BillAddRequest(customerId, Type, ReservationOrDeliveryId);
            var response = await _httpClient.PostAsJsonAsync("api/Bills/Post", request);
            try
            {
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                content = content.Trim('\"');
                var result = Guid.Parse(content);
                _navManager.NavigateTo($"/billData/{result}");
            }
            catch (Exception ex)
            {
                _toastService.ShowError($"Помилка! {ex.Message}");
                _navManager.NavigateTo("/");
            }
        }

        public async Task<BillResponse> Get(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Bills/Get/{id}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BillResponse>(responseBody);
            return result;
        }

        public async Task<bool> Delete(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Bills/Delete/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<ICollection<BillResponse>> GetBillsOfCustomer(int pageIndex, int pageSize, Guid customerId)
        {
            var response = await _httpClient.GetAsync($"api/Bills/GetBillsOfCustomer?pageIndex={pageIndex}&pageSize={pageSize}&CustomerId={customerId}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ICollection<BillResponse>>(responseBody);
            return result;
        }

        public async Task<ICollection<BillResponse>> GetAll()
        {
            var response = await _httpClient.GetAsync($"api/Bills/GetAll");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ICollection<BillResponse>>(responseBody);
            return result;
        }

        public async Task<BillResponseForAdmin> GetForAdmin(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Bills/GetForAdmin/{id}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BillResponseForAdmin>(responseBody);
            return result;
        }

        public async Task PayBill(BillPayRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync("api/Bills/Pay", request);
            var rest = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                _toastService.ShowSuccess("Успішно оплачено!");
                _navManager.NavigateTo("/");
            }
            else
            {
                _toastService.ShowError("Помилка при оплаті! " + rest);
            }
        }
    }
}
