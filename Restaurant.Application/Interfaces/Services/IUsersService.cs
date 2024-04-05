using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Interfaces.Services
{
    public interface IUsersService
    {
        Task Register(string email, string password, string phoneNum);
        Task<string> Login(string email, string password);
    }
}
