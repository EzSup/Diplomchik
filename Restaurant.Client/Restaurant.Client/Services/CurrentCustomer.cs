using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Restaurant.Core;
using Restaurant.Core.DTOs.SmallDtos;
using Restaurant.Core.Functions;
using Restaurant.Core.Services.Interfaces;
using System.Security.Claims;
using Mapster;
using Restaurant.Core.DTOs;

namespace Restaurant.Client.Services
{
    public class CurrentCustomer : AuthenticationStateProvider
    {
        public CurrentCustomerData? Info { get; private set; }
        private readonly ICustomersService _service;

        public CurrentCustomer(ICustomersService service)
        {
            _service = service;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            if (Info == null)
            {
                var user = new ClaimsPrincipal(new ClaimsIdentity());

                return Task.FromResult(new AuthenticationState(user));
            }
            else
            {
                var identity = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, Info?.Id.ToString() ?? ""),
            }, "RestaurantAuth");

                var user = new ClaimsPrincipal(identity);

                return Task.FromResult(new AuthenticationState(user));
            }
        }

        public async Task<LogInResult> LogInAsync(CustomerLogInDto dto)
        {
            var obj = (await _service.GetAll()).SingleOrDefault(x => x.Email == dto.Email);
            if (obj == null || !Security.PasswordsMatch(dto.Password, obj.Password))
            {
                return new LogInResult(false, "Email address and/or password is not correct");
            }

            if (Security.PasswordNeedsRehash(obj.Password))
            {
                obj.Password = Security.HashPassword(dto.Password);
                await _service.Update(obj);                
            }

            Info = new(obj.Id, obj.Name);

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

            return new(true, "Successfully logged in.");
        }

        public Task LogOutAsync()
        {
            Info = null;

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

            return Task.CompletedTask;
        }

        public sealed record CurrentCustomerData(int Id, string Name);
        public sealed record LogInResult(bool Success, string Message);
    }
}
