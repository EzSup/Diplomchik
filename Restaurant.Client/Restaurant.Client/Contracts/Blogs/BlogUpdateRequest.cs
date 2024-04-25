namespace Restaurant.Client.Contracts.Blogs
{
    public class BlogUpdateRequest
    {
        public Guid Id { get; set; }
        public string? AuthorName { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? ImageLink { get; set; }
        public DateTime? Created { get; set; }
    }
}
