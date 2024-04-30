namespace Restaurant.API.Contracts.Reviews
{
    public record ReviewUpdateRequest(
        Guid Id,
        string? Title,
        string? Content,
        double Rate);
}
