using Restaurant.Core.Dtos.DependingDtos;

namespace Restaurant.Core.Dtos;

public class CategoryDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int? DiscountId { get; set; }
    public ICollection<DishNoRelatedDto> Dishes { get; set; }
}