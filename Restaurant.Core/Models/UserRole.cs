using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Models
{
    public class UserRole
    {
        public int RoleId { get; set; }
        public Guid UserId { get; set; }
    }
}
