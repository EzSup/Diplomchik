using Restaurant.Client.Contracts.Users;
using Restaurant.Client.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Mapster;
using Microsoft.AspNetCore.Components;
using Restaurant.Client.Services.Interfaces;
using Restaurant.Client.Contracts.Customers;

namespace Restaurant.Client.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly CustomAuthenticationStateProvider _authStateProv;
        private readonly IJSRuntime _jsruntime;
        private readonly NavigationManager _navManager;
        private readonly ICustomersService _customersService;
        private readonly ICookiesService _cookiesService;

        public AccountService(IHttpClientFactory factory, 
            AuthenticationStateProvider authstateprov,
            IJSRuntime jsRuntime, 
            NavigationManager navManager,
            ICookiesService cookiesService,
            ICustomersService customersService)
        {
            _httpClient = factory.CreateClient("API");
            _authStateProv = (CustomAuthenticationStateProvider)authstateprov;
            _jsruntime = jsRuntime;
            _navManager = navManager;
            _cookiesService = cookiesService;
            _customersService = customersService;
        }

        public async Task LoginAsync(LoginUserRequest model)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Users/Login", model);
            var result = await response.Content.ReadFromJsonAsync<LoginUserResponse>();
            if (!result!.Flag)
            {
                await _jsruntime.InvokeVoidAsync("alert", result.Message);
                return;
            }
            await _authStateProv.UpdateAuthenticationState(result.JWTToken);
            if (result.Flag)
            {
                _navManager.NavigateTo("/", forceLoad: true);
            }
        }

        public async Task RegisterAsync(RegisterDto model)
        {
            //RegisterUserRequest request = model.Adapt<RegisterUserRequest>();
            RegisterUserRequest request = new(model.Email, model.Password, model.PhoneNum);
            var response = await _httpClient.PostAsJsonAsync("api/Users/Register", request);
            var result = await response.Content.ReadFromJsonAsync<RegisterUserResponse>();
            string message = String.Concat(result.Flag ? "Успіх!" : "Помилка!",
                result.Message);
            await _jsruntime.InvokeVoidAsync("alert", message);
            var id = result.Id;
            CustomerCreateRequest customerCreateRequest = new CustomerCreateRequest()
            {
                Name = model.Name,
                UserId = (Guid)id
            };
            if (result.Flag && await _customersService.Add(customerCreateRequest))
            {
                _navManager.NavigateTo("/login", forceLoad: true);
            }
        }

        public async Task LogoutAsync()
        {
            var response = await _httpClient.DeleteAsync("api/Users/Logout");
            //_cookiesService.DeleteCookie("tasty-cookies");
            await _authStateProv.UpdateAuthenticationState(null);
        }
    }
}
