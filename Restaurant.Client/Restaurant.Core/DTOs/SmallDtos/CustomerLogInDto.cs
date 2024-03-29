using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.DTOs.SmallDtos
{
    public class CustomerLogInDto
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public CustomerLogInDto()
        {
            
        }

        public CustomerLogInDto(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
