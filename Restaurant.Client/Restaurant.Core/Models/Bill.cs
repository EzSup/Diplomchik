using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Restaurant.Core.Models;

public class Bill
{
    public int Id { get; set; }
    public decimal Price { get; set; }
    public decimal PaidAmount { get; set; }
    public DateTime OrderDateAndTime { get; set; }
    public int TipsPercents { get; set; }
    public int? TableId { get; set; }
    public int? CustomerId { get; set; }
    public int? CartId { get; set; }
    public Table? Table { get; set; }
    public Customer? Customer { get; set; }
    public Cart? Cart { get; set; }
}