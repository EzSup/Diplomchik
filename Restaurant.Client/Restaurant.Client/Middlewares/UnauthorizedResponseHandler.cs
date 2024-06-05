using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Blazored.Toast.Services;
using Restaurant.Client.Auth;
using Microsoft.AspNetCore.Components;
using Restaurant.Client.Services.Interfaces;

public class UnauthorizedResponseHandler : DelegatingHandler
{
    private readonly IAccountService _accountService;

    public UnauthorizedResponseHandler(IAccountService accountService)
    {
        _accountService = accountService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
        {
            await _accountService.LogoutAsync();
        }

        return response;
    }
}
