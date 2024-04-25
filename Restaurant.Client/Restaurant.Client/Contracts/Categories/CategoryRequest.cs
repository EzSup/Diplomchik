namespace Restaurant.Client.Contracts.Categories
{
    public class CategoryRequest {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? DiscountId { get; set; }
    }
}
