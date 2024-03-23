namespace Restaurant.Core.Models;

public class Customer
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? PhoneNum { get; set; }
    public string? Email { get; set; }
    public string? PhotoLink { get; set; }
    public string Password { get; set; }
    public int? CartId { get; set; }
    
    public ICollection<Review> Reviews { get; set; }
    public ICollection<Bill> Bills { get; set; }
    public Cart? Cart { get; set; }
}