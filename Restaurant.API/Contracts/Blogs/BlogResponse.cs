namespace Restaurant.API.Contracts.Blogs
{
    public record BlogResponse(
        Guid Id,
        string Title,
        string AuthorName,
        string Content,
        DateTime Created
    );
}
