using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Client.Auth
{
    public record CustomUserClaims(string userId = null!, string Email = null!, string Policy = null!);
}
