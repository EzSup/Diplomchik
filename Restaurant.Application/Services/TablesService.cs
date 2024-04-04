using Restaurant.Application.Interfaces.Repositories;
using Restaurant.Application.Interfaces.Services;
using Restaurant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Table = Restaurant.Core.Models.Table;

namespace Restaurant.Application.Services
{
    public class TablesService : ITablesService
    {
        private readonly ITablesRepository _tablesRepository;

        public TablesService(ITablesRepository tablesRepository)
        {
            _tablesRepository = tablesRepository;
        }

        public async Task<ICollection<Table>> GetAll() => await _tablesRepository.GetAll();
        public async Task<Table> GetById(Guid Id) => await _tablesRepository.GetById(Id);
        public async Task<ICollection<Table>> GetByPage(int page, int pageSize) => await _tablesRepository.GetByPage(page,pageSize);
        public async Task<ICollection<Table>> GetWithBills() => await _tablesRepository.GetWithBills();
        public async Task<ICollection<Table>> GetByFilter(bool? isAvailable, decimal minPrice, decimal maxPrice) => await _tablesRepository.GetByFilter(isAvailable, minPrice, maxPrice);
        public async Task<Guid> Add(Table obj) => await _tablesRepository.Add(obj);
        public async Task<bool> Update(Table obj) => await _tablesRepository.Update(obj);
        public async Task<bool> Delete(Guid id) => await _tablesRepository.Delete(id);
        public async Task<int> Purge(IEnumerable<Guid> ids) => await _tablesRepository.Purge(ids);
    }
}
