namespace Restaurant.Core.Models;

public class Table
{
    public Guid Id { get; set; }
    public decimal PriceForHour { get; set; }
    public int Persons { get; set; } = 1;
    public int Num {  get; set; }
    public bool Free { get; set; } = true;

    public ICollection<Reservation> Reservations { get; set; } = [];
}