using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Restaurant.Core.Dtos.DependingDtos;

namespace Restaurant.Core.Dtos;

public class DiscountDto
{
    public int Id { get; set; }
    public double PecentsAmount { get; set; }
    public ICollection<CategoryNoRelatedDto> Categories { get; set; }
    public CuisineNoRelatedDto? Cuisine { get; set; }
    public ICollection<DishNoRelatedDto> Dishes { get; set; }
}