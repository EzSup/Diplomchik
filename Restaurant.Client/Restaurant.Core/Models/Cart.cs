using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Models
{
    public class Cart
    {
        public int Id { get; set; }
        //public IDictionary<Dish, int> dishes { get; set; } = new Dictionary<Dish, int>();
        
        public Customer? Customer { get; set; }
        public Bill? Bill { get; set; }
        public ICollection<DishCart> DishCarts { get; set; }
        

        public decimal CalculatePrice()
        {
            decimal price = 0;
            foreach (var d in DishCarts)
            {
                price += d.Dish.Price * d.Count;
            }
            return price;
        }
    }
}
