using Restaurant.Core.Dtos.Delivery;
using Restaurant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Interfaces.Services
{
    public interface IDeliveriesService
    {
        Task<ICollection<DeliveryData>> GetAll();
        Task<DeliveryData> GetById(Guid id);
        Task<Guid> Add(DeliveryAddRequest entity);
        Task<bool> Update(DeliveryData entity);
        Task<bool> Delete(Guid id);
        Task<int> Purge(IEnumerable<Guid> values);
        Task<ICollection<DeliveryData>> GetByPage(int page, int pageSize);
        Task<ICollection<DeliveryData>> GetByCustomerId(int page, int pageSize, Guid customerId);
    }
}
