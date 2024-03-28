namespace Restaurant.Core.Dtos;

public class TableDto : TableForCreateDto
{
    public int Id { get; set; }
    public bool Free { get; set; }
}