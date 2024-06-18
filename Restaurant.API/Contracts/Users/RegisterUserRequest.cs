using System.ComponentModel.DataAnnotations;

namespace Restaurant.API.Contracts.Users
{
    public record RegisterUserRequest(
        [Required] string email,
        [Required] string password,
        [Required] string phoneNumber);
}
