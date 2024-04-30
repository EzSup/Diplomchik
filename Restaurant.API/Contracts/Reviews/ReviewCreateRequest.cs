namespace Restaurant.API.Contracts.Reviews
{
    public record ReviewCreateRequest
        (Guid AuthorId,
        Guid DishId,
        string? Title,
        string? Content,
        double Rate);
}
