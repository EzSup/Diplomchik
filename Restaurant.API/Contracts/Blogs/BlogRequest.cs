namespace Restaurant.API.Contracts.Blogs
{
    public record BlogRequest(
        string AuthorName,
        string Title,
        string Content);
}
