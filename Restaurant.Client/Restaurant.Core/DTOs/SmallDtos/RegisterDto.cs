using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.DTOs.SmallDtos
{
    public class RegisterDto : LoginDto
    {
        [Required]
        public string? Name { get; set; }
        [Required, DataType(DataType.PhoneNumber), Phone]
        public string? PhoneNum { get; set; } = string.Empty;
        [Required, Compare(nameof(Password)), DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
