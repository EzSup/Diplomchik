using Restaurant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Interfaces
{
    public interface IDeliveryDatasRepository : ICrudRepository<DeliveryData>
    {
        Task<ICollection<DeliveryData>> GetByFilter(int page, int pageSize, Guid? customerId = null);
    }
}
