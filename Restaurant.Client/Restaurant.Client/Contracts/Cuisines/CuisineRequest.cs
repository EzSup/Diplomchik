namespace Restaurant.Client.Contracts.Cuisines
{
    public class CuisineRequest{
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? DiscountId { get; set; }
    }
}
