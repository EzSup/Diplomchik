namespace Restaurant.API.Contracts.Carts
{
    public record AddDishRequest(Guid customerId, Guid dishId,int count);
}
