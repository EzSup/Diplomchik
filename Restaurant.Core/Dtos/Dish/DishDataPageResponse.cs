using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Dtos.Dish
{
    public class DishDataPageResponse
    {
        public Guid Id {  get; set; }
        public string Name { get; set; } = string.Empty;
        public double Weight { get; set; }
        public ICollection<string?> IngredientsList { get; set; } = [];
        public bool Available { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal ResultingPrice { get; set; }
        public ICollection<string> PhotoLinks { get; set; } = [];
        public double DiscountPercents { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Cuisine { get; set; } = string.Empty;
        public int ReviewsCount { get; set; }
        public double Rating { get; set; }
    }
}
