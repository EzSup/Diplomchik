using Restaurant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Interfaces.Services
{
    public interface IBillsService
    {
        Task<ICollection<Bill>> GetAll();
        Task<Bill> GetById(Guid id);
        Task<Guid> Add(Bill entity);
        Task<bool> Update(Bill entity);
        Task<bool> Delete(Guid id);
        Task<int> Purge(IEnumerable<Guid> values);
        Task<ICollection<Bill>> GetByPage(int page, int pageSize);
        Task<ICollection<Bill>> GetByFilter(int pageIndex, int pageSize, decimal MinPrice = 0, decimal MaxPrice = decimal.MaxValue,
            DateTime? minOrderDateTime = null, DateTime? maxOrderDateTime = null,
            Bill.OrderType? orderType = null, int minTipsPercents = 0, int maxTipsPercents = 100,
            Guid? customerId = null, Guid? reservationId = null, Guid? deliveryId = null);
    }
}
