using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Models
{
    public class DishCart
    {
        public Guid DishId { get; set; }
        public Guid CartId { get; set; }

        public int Count { get; set; } = 1;

        public Dish? Dish { get; set; }
        public Cart? Cart { get; set; }
    }
}
