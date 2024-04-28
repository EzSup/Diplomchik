using Restaurant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Interfaces.Services
{
    public interface ICuisinesService
    {
        Task<ICollection<Cuisine>> GetAll();
        Task<Cuisine> GetById(Guid id);
        Task<Guid> Add(Cuisine entity);
        Task<bool> Update(Cuisine entity);
        Task<bool> Delete(Guid id);
        Task<int> Purge(IEnumerable<Guid> values);
        Task<ICollection<Cuisine>> GetByPage(int page, int pageSize);
        Task<bool> AddDiscount(Guid cuisineId, double PercentsAmount);
        Task<bool> RemoveDiscount(Guid cuisineId);
    }
}
