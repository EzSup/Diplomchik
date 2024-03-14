using Restaurant.Core.Dtos.DependingDtos;

namespace Restaurant.Core.Dtos;

public class CustomerDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? PhoneNum { get; set; }
    public string? Email { get; set; }
    public string? PhotoLink { get; set; }
    public string? Password { get; set; }
    public ICollection<ReviewNoRelatedDto> Reviews { get; set; }
    public ICollection<BillNoRelatedDto> Bills { get; set; }
}