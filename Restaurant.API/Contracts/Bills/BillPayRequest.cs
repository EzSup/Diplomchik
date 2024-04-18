namespace Restaurant.API.Contracts.Bills
{
    public record BillPayRequest(Guid BillId, decimal Amount, int TipsPercents);
}
