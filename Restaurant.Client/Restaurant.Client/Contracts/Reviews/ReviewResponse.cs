namespace Restaurant.Client.Contracts.Reviews
{
    public record ReviewResponse(
        Guid Id,
        string? Title,
        string? Content,
        double Rate,
        DateTime Posted,
        Guid DishId,
        Guid AuthorId
    );
}
