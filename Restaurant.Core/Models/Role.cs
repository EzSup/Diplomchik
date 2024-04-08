using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<Permission> Permissions { get; set; } = [];
        public ICollection<User> Users { get; set; } = [];

        public override string ToString()
        {
            return Name;
        }
    }

    
}
