using Restaurant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Interfaces.Services
{
    public interface ICategoriesService
    {
        Task<ICollection<Category>> GetAll();
        Task<Category> GetById(Guid id);
        Task<Guid> Add(Category entity);
        Task<bool> Update(Category entity);
        Task<bool> Delete(Guid id);
        Task<int> Purge(IEnumerable<Guid> values);
        Task<ICollection<Category>> GetByPage(int page, int pageSize);
        Task<bool> AddDiscount(Guid categoryId, double PercentsAmount);
        Task<bool> RemoveDiscount(Guid categoryId);
    }
}
