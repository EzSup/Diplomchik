using Restaurant.Application.Interfaces.Services;
using Restaurant.Core.Interfaces;
using Restaurant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Services
{
    public class BillsService : IBillsService
    {
        private readonly IBillsRepository _billsRepository;

        public BillsService(IBillsRepository repository)
        {
            _billsRepository = repository; 
        }

        public async Task<ICollection<Bill>> GetAll() => await _billsRepository.GetAll();
        public async Task<Bill> GetById(Guid Id) => await _billsRepository.GetById(Id);
        public async Task<ICollection<Bill>> GetByPage(int page, int pageSize) => await _billsRepository.GetByPage(page, pageSize);
        public async Task<ICollection<Bill>> GetByFilter(int pageIndex, int pageSize, decimal MinPrice = 0, decimal MaxPrice = decimal.MaxValue,
            DateTime? minOrderDateTime = null, DateTime? maxOrderDateTime = null,
            Bill.OrderType? orderType = null, int minTipsPercents = 0, int maxTipsPercents = 100,
            Guid? customerId = null, Guid? reservationId = null, Guid? deliveryId = null)
            => await _billsRepository.GetByFilter(pageIndex, pageSize, MinPrice, MaxPrice,
            minOrderDateTime, maxOrderDateTime,
            orderType, minTipsPercents, maxTipsPercents,
            customerId,reservationId ,deliveryId );

        public async Task<Guid> Add(Bill obj) => await _billsRepository.Add(obj);
        public async Task<bool> Update(Bill obj) => await _billsRepository.Update(obj);
        public async Task<bool> Delete(Guid id) => await _billsRepository.Delete(id);
        public async Task<int> Purge(IEnumerable<Guid> ids) => await _billsRepository.Purge(ids);
    }
}
