namespace Restaurant.Client.Contracts.Reviews
{
    public class ReviewCreateRequest
    {
        public Guid DishId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public int Rate { get; set; } = 5;
    }
}
