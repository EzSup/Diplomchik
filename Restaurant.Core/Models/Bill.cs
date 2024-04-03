using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Restaurant.Core.Models;

public class Bill
{
    public Guid Id { get; set; }

    public decimal TotalPrice { get; set; }
    public decimal PaidAmount { get; set; }
    public DateTime OrderDateAndTime { get; set; }
    public int TipsPercents { get; set; }

    public Guid TableId { get; set; }
    public Guid CustomerId { get; set; }
    public Guid CartId { get; set; }

    public Table? Table { get; set; }
    public Customer? Customer { get; set; }
    public Cart? Cart { get; set; }
}