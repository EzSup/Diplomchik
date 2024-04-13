namespace Restaurant.Client.Contracts.Carts
{
    public record AddDishRequest(Guid customerId, Guid dishId, int count);
}
