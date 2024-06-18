namespace Restaurant.API.Contracts.Reviews
{
    public class ReviewResponse
    {
        public Guid Id { get; set; }
        public string? Title { get; set; } = string.Empty;
        public string? Content { get; set; } = string.Empty;
        public double Rate { get; set; } = 0;
        public DateTime Posted { get; private set; }
        public Guid DishId { get; set; }
        public Guid AuthorId { get; set; }
    }
}
