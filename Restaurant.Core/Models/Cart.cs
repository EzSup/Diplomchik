using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Models
{
    public class Cart
    {
        public Guid Id { get; set; }

        public Customer? Customer { get; set; }
        public Bill? Bill { get; set; }
        public ICollection<DishCart> DishCarts { get; set; } = [];

        public decimal CalculatePrice()
        {
            decimal price = 0;
            foreach (var d in DishCarts)
            {
                price += (d?.Dish?.Price ?? 0) * d?.Count ?? 0;
            }
            return price;
        }
    }
}
