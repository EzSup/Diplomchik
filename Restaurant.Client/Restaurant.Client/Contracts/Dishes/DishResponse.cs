namespace Restaurant.Client.Contracts.Dishes
{
    public record DishResponse(
        Guid Id,
        string Name,
        double Weight,
        ICollection<string?> IngredientsList,
        bool Available,
        decimal Price,
        ICollection<string> PhotoLinks,
        double DiscountPercents,
        Guid CategoryId,
        Guid CuisineId);
}
