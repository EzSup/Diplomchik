using Restaurant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Interfaces
{
    public interface IReservationsRepository : ICrudRepository<Reservation>
    {
        Task<ICollection<Reservation>> GetByFilter(DateTime startDateFrom, DateTime endDateTill = new DateTime(), Guid? tableId = null);
    }
}
