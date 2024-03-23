using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Models
{
    public class DishCart
    {
        public int DishId { get; set; }
        public int CartId { get; set; }

        public int Count { get; set; }
        public Dish? Dish { get; set; }
        public Cart? Cart { get; set; }
    }
}
