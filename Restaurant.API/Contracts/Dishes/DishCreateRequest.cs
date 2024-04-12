namespace Restaurant.API.Contracts.Dishes
{
    public record DishCreateRequest(
        string Name,
        double? Weight,
        ICollection<string?> IngredientsList,
        bool? Available,
        decimal Price,
        ICollection<string?> PhotoLinks,
        Guid CategoryId,
        Guid CuisineId);
}
