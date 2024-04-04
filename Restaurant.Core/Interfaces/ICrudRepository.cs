using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Interfaces
{
    public interface ICrudRepository<Entity>
    {
        Task<ICollection<Entity>> GetAll();
        Task<Entity> GetById(Guid id);
        Task<Guid> Add(Entity entity);
        Task<bool> Update(Entity entity);
        Task<bool> Delete(Guid id);
        Task<int> Purge(IEnumerable<Guid> values);
        Task<ICollection<Entity>> GetByPage(int page, int pageSize);
    }
}
