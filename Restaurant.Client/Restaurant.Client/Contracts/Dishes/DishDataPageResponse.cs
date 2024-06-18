namespace Restaurant.Client.Contracts.Dishes
{
    public record DishDataPageResponse(
        Guid Id,
        string Name,
        double Weight,
        ICollection<string?> IngredientsList,
        bool Available,
        decimal OriginalPrice,
        decimal ResultingPrice,
        ICollection<string> PhotoLinks,
        double DiscountPercents,
        string Category,
        string Cuisine,
        int ReviewsCount,
        double Rating
        );
}
