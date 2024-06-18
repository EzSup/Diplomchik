using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Models
{
    public class Reservation
    {
        public Guid Id { get; set; }        
        public DateTime Start {  get; set; }
        public DateTime End { get; set; }

        public Guid? TableId { get; set; }

        public Table? Table { get; set; }
        public Bill? Bill { get; set; }
    }
}
