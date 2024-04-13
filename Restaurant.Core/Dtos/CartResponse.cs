namespace Restaurant.Core.Dtos
{
    public class CartResponse
    {
        public Guid Id { get; set; }
        public ICollection<DishOfCart> Dishes { get; set; } = [];
    }
    public record DishOfCart(Guid Id, string Name, string PhotoLink, int count);
}
