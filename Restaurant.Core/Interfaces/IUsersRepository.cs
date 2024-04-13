using Restaurant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Interfaces
{
    public interface IUsersRepository
    {
        Task<Guid> Add(User user);
        Task<User> GetByEmail(string email);
        Task<HashSet<Core.Enums.Permission>> GetUserPermissions(Guid userId);
    }
}
