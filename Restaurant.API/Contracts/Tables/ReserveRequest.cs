namespace Restaurant.API.Contracts.Tables
{
    public record ReserveRequest(Guid tableId, DateTime start);
}
