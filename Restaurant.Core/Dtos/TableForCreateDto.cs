﻿namespace Restaurant.Core.Dtos;

public class TableForCreateDto
{
    public decimal PriceForHour { get; set; }
    public int? BillId { get; set; }
}