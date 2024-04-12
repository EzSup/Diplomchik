namespace Restaurant.Client.Contracts.Dishes
{
    public class DishPaginationRequest
    {
        public int pageIndex {  get; set; }
        public int pageSize { get; set; }
        public string? Name { get; set; } = string.Empty;
        public double MinWeight { get; set; } = 0;
        public double MaxWeight { get; set; } = 5000;
        public IEnumerable<string>? Ingredients { get; set; } = [];
        public bool? Available { get; set; } = true;
        public decimal MinPrice { get; set; } = 0;
        public decimal MaxPrice { get; set; } = 5000;
        public string? Category { get; set; } = string.Empty;
        public string? Cuisine { get; set; } = string.Empty;
        public double MinDiscountsPercents { get; set; } = 0;
    }
}
