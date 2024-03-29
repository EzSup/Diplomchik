namespace Restaurant.Core.DTOs;

public class TableDto : TableForCreateDto
{
    public int Id { get; set; }
    public bool Free { get; set; }
}