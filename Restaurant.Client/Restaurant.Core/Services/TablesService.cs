using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Restaurant.Core.Dtos;
using Restaurant.Core.Repositories.Interfaces;
using Restaurant.Core.Services.Interfaces;
using Restaurant.Core.Models;
using Mapster;

namespace Restaurant.Core.Services
{
    public class TablesService : ITablesService
    {
        private ITablesRepository _tablesRepository;
        public TablesService(ITablesRepository tablesRepository)
        {
            _tablesRepository = tablesRepository;
        }

        public async Task<ICollection<Models.Table>> GetAll() => await _tablesRepository.GetAll();
        public async Task<Models.Table?> Get(int id) => await _tablesRepository.Get(id);
        public async Task<bool> Update(TableDto dto) => await _tablesRepository.Update(dto);
        public async Task<int> Create(TableForCreateDto dto) => await _tablesRepository.Create(dto);
        public async Task<bool> Delete(int id) => await _tablesRepository.Delete(id);
        public async Task SwitchFree(int id, bool free)
        {
            var table = await Get(id);
            var tableDto = table.Adapt<TableDto>();
            if (free == table?.Free)
            {
                return;
            }
            tableDto.Free = free;
            await Update(tableDto);
        }
    }
}
