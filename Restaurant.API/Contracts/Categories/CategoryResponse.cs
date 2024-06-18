namespace Restaurant.API.Contracts.Categories
{
    public class CategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double DiscountPercents { get; set; }
    }
}
