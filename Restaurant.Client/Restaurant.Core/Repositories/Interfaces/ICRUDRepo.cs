using Restaurant.Core.DTOs;
using Restaurant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Repositories.Interfaces
{
    public interface ICRUDRepo<ObjClass, ForCreateDto> where ObjClass : class where ForCreateDto : class
    {
        Task<ICollection<ObjClass>> GetAll();
        Task<ObjClass?> Get(int id);
        Task<int> Create(ForCreateDto dto);
        Task<bool> Update(ObjClass dto);
        Task<bool> Delete(int id);
    }
}
