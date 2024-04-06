using Restaurant.Client.Contracts.Users;

namespace Restaurant.Core.Services.Interfaces
{
    public interface IAccountService
    {
        Task RegisterAsync(RegisterDto model);
        Task LoginAsync(LoginUserRequest model);
        Task LogoutAsync();
    }
}
