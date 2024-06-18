using Restaurant.Client.Contracts.Enums;
using Restaurant.Client.Contracts.Bill;

namespace Restaurant.Client.Services.Interfaces
{
    public interface IBillsService
    {
        Task AddBill(Guid ReservationOrDeliveryId, OrderType Type);
        Task PayBill(BillPayRequest request);
        Task<BillResponse> Get(Guid id);
        Task<ICollection<BillResponse>> GetBillsOfCustomer(int pageIndex, int pageSize, Guid customerId);
        Task<ICollection<BillResponse>> GetAll();
        Task<BillResponseForAdmin> GetForAdmin(Guid id);
        Task<bool> Delete(Guid id);
    }
}
