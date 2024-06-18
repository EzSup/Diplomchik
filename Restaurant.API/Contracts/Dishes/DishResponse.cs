namespace Restaurant.API.Contracts.Dishes
{
    public class DishResponse {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Weight { get; set; }
        public ICollection<string?> IngredientsList { get; set; }
        public bool Available { get; set; }
        public decimal Price { get; set; }
        public ICollection<string?> PhotoLinks { get; set; }
        public double DiscountPercents { get; set; }
        public Guid CategoryId { get; set; }
        public Guid CuisineId { get; set; }
    };
}
