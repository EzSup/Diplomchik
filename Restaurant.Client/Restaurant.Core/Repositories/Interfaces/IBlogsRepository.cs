using Restaurant.Core.DTOs.ForCreate;
using Restaurant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Repositories.Interfaces
{
    public interface IBlogsRepository : ICRUDRepo<Blog, BlogForCreateDto>
    {
    }
}
