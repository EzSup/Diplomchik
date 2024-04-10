﻿namespace Restaurant.API.Contracts.Dishes
{
    public record DishPaginationRequest(
        string? Name = null,
            double MinWeight = 0,
            double MaxWeight = double.MaxValue,
            IEnumerable<string>? Ingredients = null,
            bool? Available = null,
            decimal MinPrice = 0,
            decimal MaxPrice = decimal.MaxValue,
            string? Category = null,
            string? Cuisine = null,
            double MinDiscountsPercents = 0);
}
