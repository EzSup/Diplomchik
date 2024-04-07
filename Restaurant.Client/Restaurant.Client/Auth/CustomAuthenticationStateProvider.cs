using Microsoft.AspNetCore.Components.Authorization;
using Restaurant.Client.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;

namespace Restaurant.Client.Auth
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private const string tokenName = "jwtToken";
        private readonly ClaimsPrincipal anonymous = new(new ClaimsIdentity());
        private readonly ICookiesService _cookiesService;
        private readonly Blazored.LocalStorage.ILocalStorageService _localStorageService;

        public CustomAuthenticationStateProvider(ICookiesService cookies,
            Blazored.LocalStorage.ILocalStorageService localStorageService)
        {
            _cookiesService = cookies;
            _localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(await _localStorageService.GetItemAsStringAsync(tokenName)))
                    return await Task.FromResult(new AuthenticationState(anonymous));

                var getUserClaims = DecryptToken(await _localStorageService.GetItemAsStringAsync(tokenName));
                if (getUserClaims == null)
                    return await Task.FromResult(new AuthenticationState(anonymous));

                var claimsPrincipal = SetClaimPrincipal(getUserClaims);
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }
            catch
            {
                return await Task.FromResult(new AuthenticationState(anonymous));
            }
        }

        public async Task UpdateAuthenticationState(string jwtToken)
        {
            var claimsPrincipal = new ClaimsPrincipal();
            if (!string.IsNullOrEmpty(jwtToken))
            {
                await _localStorageService.SetItemAsStringAsync(tokenName, jwtToken);
                //Constants.JWTToken = jwtToken;
                var getUserClaims = DecryptToken(jwtToken);
                claimsPrincipal = SetClaimPrincipal(getUserClaims);
            }
            else
            {
                await _localStorageService.RemoveItemAsync(tokenName);
                //Constants.JWTToken = null!;
            }
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public static ClaimsPrincipal SetClaimPrincipal(CustomUserClaims claims)
        {
            if (claims.userId is null)
                return new ClaimsPrincipal();

            return new ClaimsPrincipal(new ClaimsIdentity(
                new List<Claim>
                {
                    new(ClaimTypes.Name, claims.userId!),
                    new(ClaimTypes.Email, claims.Email)
                }, "JwtAuth"));
        }

        public static CustomUserClaims DecryptToken(string jwtToken)
        {
            if (string.IsNullOrEmpty(jwtToken))
                return new CustomUserClaims();

            var handler = new JwtSecurityTokenHandler();
            var token = handler.ReadJwtToken(jwtToken);

            var userId = token.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.NameIdentifier);
            var email = token.Claims.FirstOrDefault(_ => _.Type == ClaimTypes.Email);

            return new CustomUserClaims(userId.Value ?? "", email.Value ?? "");
        }
    }
}
