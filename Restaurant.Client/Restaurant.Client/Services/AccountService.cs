using Restaurant.Core.Services.Interfaces;
using Restaurant.Client.Contracts.Users;
using Restaurant.Client.Auth;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Mapster;
using Microsoft.AspNetCore.Components;

namespace Restaurant.Core.Services
{
    public class AccountService : IAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly CustomAuthenticationStateProvider _authStateProv;
        private readonly IJSRuntime _jsruntime;
        private readonly NavigationManager _navManager;

        public AccountService(IHttpClientFactory factory, 
            AuthenticationStateProvider authstateprov,
            IJSRuntime jsRuntime, 
            NavigationManager navManager)
        {
            _httpClient = factory.CreateClient("API");
            _authStateProv = (CustomAuthenticationStateProvider)authstateprov;
            _jsruntime = jsRuntime;
            _navManager = navManager;
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
            _authStateProv.UpdateAuthenticationState();
            if(result.Flag)
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
            if(result.Flag)
            {
                _navManager.NavigateTo("/login", forceLoad: true);
            }
        }

        public async Task LogoutAsync()
        {
            var response = await _httpClient.PutAsJsonAsync("api/Users/Logout","");
            _authStateProv.UpdateAuthenticationState();
        }
    }
}
