using System.ComponentModel.DataAnnotations;

namespace Restaurant.Client.Contracts.Users
{
    public class RegisterDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email {  get; set; } = string.Empty;
        [Phone]
        public string PhoneNum { get; set; } = string.Empty;
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; } = string.Empty;
        [Compare("Password")]
        [Required]
        public string ConfirmPassword {  get; set; } = string.Empty;
    }
}
