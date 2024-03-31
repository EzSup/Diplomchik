using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Functions.Interfaces
{
    public interface IPasswordHasher
    {
        string HashPassword(string plainTextPassword);
        bool PasswordNeedsRehash(string hashedPassword);
        bool Verify(string plainTextPassword, string hashedPassword);
    }
}
