namespace Restaurant.Core.Models;

public class Customer
{
    public Guid Id { get; set; }
    public string? Name { get; set; } = String.Empty;
    public string? PhotoLink { get; set; } = String.Empty;

    public Guid? CartId { get; set; }
    public Guid UserId { get; set; }

    public User? User { get; set; }    
    public Cart? Cart { get; set; }
    public ICollection<Review> Reviews { get; set; } = [];
    public ICollection<Bill> Bills { get; set; } = [];
}