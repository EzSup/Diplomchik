using Restaurant.Application.Interfaces.Repositories;
using Restaurant.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;
using Table = Restaurant.Core.Models.Table;

namespace Restaurant.Application.Interfaces.Services
{
    public interface ITablesService
    {
        Task<ICollection<Table>> GetAll();
        Task<Table> GetById(Guid id);
        Task<Guid> Add(Table entity);
        Task<bool> Update(Table entity);
        Task<bool> Delete(Guid id);
        Task<int> Purge(IEnumerable<Guid> values);
        Task<ICollection<Table>> GetByPage(int page, int pageSize);
        Task<ICollection<Table>> GetWithBills();
        Task<ICollection<Table>> GetByFilter(bool? available, decimal minPrice = 0, decimal maxPrice = decimal.MaxValue);
    }
}
