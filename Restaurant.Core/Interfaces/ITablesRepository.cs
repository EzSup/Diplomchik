using Restaurant.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Table = Restaurant.Core.Models.Table;

namespace Restaurant.Application.Interfaces.Repositories
{
    public interface ITablesRepository : ICrudRepository<Table>
    {
        Task<ICollection<Table>> GetWithBills();
        Task<ICollection<Table>> GetByFilter(bool? available, decimal minPrice = 0, decimal maxPrice = decimal.MaxValue);        
    }
}
