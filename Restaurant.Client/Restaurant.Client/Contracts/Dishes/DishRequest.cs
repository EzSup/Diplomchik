namespace Restaurant.Client.Contracts.Dishes
{
    public record DishRequest(
        Guid Id,
        string Name,
        double Weight,
        ICollection<string?> IngredientsList,
        bool? Available,
        decimal Price,
        ICollection<string?> PhotoLinks,
        Guid? CategoryId,
        Guid? CuisineId,
        Guid? DiscountId);
}
