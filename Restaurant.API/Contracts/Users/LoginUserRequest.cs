using System.ComponentModel.DataAnnotations;

namespace Restaurant.API.Contracts.Users
{
    public record LoginUserRequest(
        [Required] string email,
        [Required] string password);
}
