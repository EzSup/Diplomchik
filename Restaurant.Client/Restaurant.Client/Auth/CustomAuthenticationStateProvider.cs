using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Restaurant.Client.Auth
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal anonymous = new(new ClaimsIdentity());

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(Constants.JWTToken))
                    return await Task.FromResult(new AuthenticationState(anonymous));

                var getUserClaims = DecryptToken(Constants.JWTToken);
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

        public void UpdateAuthenticationState(string jwtToken)
        {
            var claimsPrincipal = new ClaimsPrincipal();
            if (!string.IsNullOrEmpty(jwtToken))
            {
                Constants.JWTToken = jwtToken;
                var getUserClaims = DecryptToken(jwtToken);
                claimsPrincipal = SetClaimPrincipal(getUserClaims);
            }
            else
            {
                Constants.JWTToken = null!;
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
