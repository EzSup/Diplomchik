using System.ComponentModel.DataAnnotations;

namespace Restaurant.Client.Contracts.Customers
{
    public class CustomerCreateRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public Guid? CartId { get; set; }
    }
}
