using Microsoft.EntityFrameworkCore;
using Restaurant.Core.DTOs;
using Restaurant.Core.Models;
using Restaurant.Core.Repositories.Interfaces;

namespace Restaurant.Core.Repositories;

public class TablesRepository : RepositoryWithSave, ITablesRepository
{
    private readonly RestaurantDbContext _dbContext;

    public TablesRepository(RestaurantDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ICollection<Table>> GetAll()
    {
        return await _dbContext.Tables.OrderBy(x => x.Id).AsNoTracking().ToListAsync();
    }

    public async Task<Table?> Get(int id)
    {
        return await _dbContext.Tables.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> Create(TableForCreateDto dto)
    {
        var obj = new Table
        {
            PriceForHour = dto.PriceForHour,
            Free = true
        };
        _dbContext.Tables.Add(obj);
        
        if (await Save())
        {
            return obj.Id;
        }

        return 0;
    }

    public async Task<bool> Update(Table obj)
    {
        await _dbContext.Tables.Where(t => t.Id == obj.Id)
            .ExecuteUpdateAsync(t => t
            .SetProperty(t => t.PriceForHour, t => obj.PriceForHour)
            .SetProperty(t => t.Free, t => obj.Free));

        return await Save();
    }

    public async Task<bool> Delete(int id)
    {
        await _dbContext.Tables.Where(t => t.Id == id).ExecuteDeleteAsync();

        return await Save();
    }
}