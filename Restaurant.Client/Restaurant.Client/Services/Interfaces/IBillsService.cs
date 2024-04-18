using Restaurant.Client.Contracts.Enums;
using Restaurant.Client.Contracts.Bill;

namespace Restaurant.Client.Services.Interfaces
{
    public interface IBillsService
    {
        Task<Guid> AddBill(Guid ReservationOrDeliveryId, OrderType Type);
        Task PayBill(BillPayRequest request);
        Task<BillResponse> Get(Guid id);
        Task<ICollection<BillResponse>> GetBillsOfCustomer(int pageIndex, int pageSize, Guid customerId);
    }
}
