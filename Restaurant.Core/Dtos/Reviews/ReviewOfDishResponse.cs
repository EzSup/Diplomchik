using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Dtos.Reviews
{
    public class ReviewOfDishResponse
    {
        public string? Title { get; set; } = string.Empty;
        public string? Content { get; set; } = string.Empty;
        public double Rate { get; set; }
        public DateTime Posted { get; set; }
        public string? AuthorName { get; set; } = string.Empty;
    }
}
