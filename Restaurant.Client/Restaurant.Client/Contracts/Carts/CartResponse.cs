namespace Restaurant.Client.Contracts.Carts
{
    public record CartResponse(
        Guid Id,
        ICollection<DishOfCart> Dishes
    );
    public class DishOfCart { public Guid Id { get; set; } 
        public string Name { get; set; }
        public string PhotoLink { get; set; }
        public int count { get; set; } }
}
