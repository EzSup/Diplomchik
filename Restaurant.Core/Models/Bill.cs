namespace Restaurant.Core.Models;

public class Bill
{
    public Guid Id { get; set; }
    
    public decimal TotalPrice { get; set; }
    public decimal PaidAmount { get; set; } = 0;
    public DateTime OrderDateAndTime { get; set; }
    public int TipsPercents { get; set; } = 0;
    public OrderType Type { get; set; }

    public Guid CustomerId { get; set; }
    public Guid CartId { get; set; }
    public Guid? ReservationId { get; set; }
    public Guid? DeliveryId { get; set; }

    public Customer? Customer { get; set; }
    public Cart? Cart { get; set; }
    public Reservation? Reservation { get; set; }
    public DeliveryData? DeliveryData { get; set; }

    public enum OrderType
    {
        InRestaurant,
        Delivery
    }
}