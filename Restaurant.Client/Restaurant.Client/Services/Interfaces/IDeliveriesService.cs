using Restaurant.Client.Contracts.Deliveries;

namespace Restaurant.Client.Services.Interfaces
{
    public interface IDeliveriesService
    {
        public Task<Guid> RegisterDelivery(DeliveryAddRequest request);
        public Task<ICollection<DeliveryResponse>> GetDeliveriesByCustomer(int pageIndex, int pageSize, Guid customerId);
    }
}
