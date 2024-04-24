using Restaurant.Application.Interfaces.Services;
using Restaurant.Core.Dtos.Delivery;
using Restaurant.Core.Interfaces;
using Restaurant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Services
{
    public class DeliveriesService : IDeliveriesService
    {
        private readonly IDeliveryDatasRepository _repository;

        public DeliveriesService(IDeliveryDatasRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<DeliveryData>> GetAll() => await _repository.GetAll();
        public async Task<DeliveryData> GetById(Guid Id) => await _repository.GetById(Id);
        public async Task<ICollection<DeliveryData>> GetByPage(int page, int pageSize) => await _repository.GetByPage(page, pageSize);
        public async Task<ICollection<DeliveryData>> GetByCustomerId(int page, int pageSize, Guid customerId)
            => await _repository.GetByFilter(page, pageSize, customerId);

        public async Task<Guid> Add(DeliveryAddRequest obj){
            var delivery = AddRequestToDelivery(obj);
            return await _repository.Add(delivery);         
        }
        public async Task<bool> Update(DeliveryData obj) => await _repository.Update(obj);
        public async Task<bool> Delete(Guid id) => await _repository.Delete(id);
        public async Task<int> Purge(IEnumerable<Guid> ids) => await _repository.Purge(ids);

        private DeliveryData AddRequestToDelivery(DeliveryAddRequest request)
        {
            var delivery = new DeliveryData()
            {
                Region = request.Region,
                SettlementName = request.SettlementName,
                StreetName = request.StreetName,
                StreetNum = request.StreetNum
            };

            return delivery;
        }
    }
}
