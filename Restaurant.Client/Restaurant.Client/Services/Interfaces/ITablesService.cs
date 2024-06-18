using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Restaurant.Client.Contracts.Tables;

namespace Restaurant.Client.Services.Interfaces
{
    public interface ITablesService
    {
        Task<Guid> Reserve(ReserveRequest request);
        Task<ICollection<TableResponse>> GetTablesOfTime(DateTime time);
        Task<bool> Add(TableCreateRequest request);
        Task<bool> Update(TableUpdateRequest request);
        Task<bool> Delete(Guid id);
        Task<ICollection<TableResponse>> GetAll();
    }
}
