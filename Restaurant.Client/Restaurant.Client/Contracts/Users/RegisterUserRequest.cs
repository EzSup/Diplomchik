using System.ComponentModel.DataAnnotations;

namespace Restaurant.Client.Contracts.Users
{
    public record RegisterUserRequest(
        [Required] string email,
        [Required] string password,
        [Required] string phoneNumber);
}
