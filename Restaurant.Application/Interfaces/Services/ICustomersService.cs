using Restaurant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Interfaces.Services
{
    public interface ICustomersService
    {
        Task<ICollection<Customer>> GetAll();
        Task<Customer> GetById(Guid id);
        Task<Guid> Add(Customer entity);
        Task<bool> Update(Customer entity);
        Task<bool> Delete(Guid id);
        Task<int> Purge(IEnumerable<Guid> values);
        Task<bool> UpdateImage(Guid id, string ImageLink);
        Task<Customer> GetByUser(Guid userId);
    }
}
