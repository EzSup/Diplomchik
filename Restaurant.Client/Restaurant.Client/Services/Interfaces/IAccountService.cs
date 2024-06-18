using Restaurant.Client.Contracts.Users;

namespace Restaurant.Client.Services.Interfaces
{
    public interface IAccountService
    {
        Task RegisterAsync(RegisterDto model);
        Task LoginAsync(LoginUserRequest model);
        Task LogoutAsync();
    }
}
