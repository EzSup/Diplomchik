using Microsoft.AspNetCore.Components.Authorization;
using Restaurant.Client.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Restaurant.Client.Auth
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal anonymous = new(new ClaimsIdentity());
        private readonly ICookiesService _cookiesService;

        public CustomAuthenticationStateProvider(ICookiesService cookies)
        {
            _cookiesService = cookies;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var jwtToken = _cookiesService.GetCookie("tasty-cookies");
                if (string.IsNullOrEmpty(jwtToken))
                    return await Task.FromResult(new AuthenticationState(anonymous));

                var getUserClaims = DecryptToken(jwtToken);
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

        public void UpdateAuthenticationState()
        {
            var claimsPrincipal = new ClaimsPrincipal();
            var jwtToken = _cookiesService.GetCookie("tasty-cookies");
            if (!string.IsNullOrEmpty(jwtToken))
            {
                var getUserClaims = DecryptToken(jwtToken);
                claimsPrincipal = SetClaimPrincipal(getUserClaims);
            }
            else
            {
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
