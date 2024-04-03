using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Models
{
    public class User
    {
        public Guid Id { get; set; }

        public string Email { get; set; } = string.Empty;
        public string PhoneNum { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        public Customer? Customer {  get; set; }
    }
}
