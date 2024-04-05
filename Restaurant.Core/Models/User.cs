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

        public string Email { get; set; }
        public string PhoneNum { get; set; }
        public string PasswordHash { get; private set; }

        public Customer? Customer {  get; set; }

        public User(Guid id, string email, string passwordHash, string phoneNum)
        {
            Id = id;
            Email = email;
            PhoneNum = phoneNum;
            PasswordHash = passwordHash;
        }

        public static User Create(Guid id, string email, string passwordHash, string phoneNum)
        {
            return new User(id, email, passwordHash, phoneNum);
        }
    }
}
