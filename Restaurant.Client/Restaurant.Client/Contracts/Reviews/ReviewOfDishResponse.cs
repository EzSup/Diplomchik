namespace Restaurant.Client.Contracts.Reviews
{
    public record ReviewOfDishResponse(
        string Title, string Content, double Rate, DateTime Posted, string AuthorName);
}
