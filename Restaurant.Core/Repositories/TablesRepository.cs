using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Dtos;
using Restaurant.Core.Models;
using Restaurant.Core.Repositories.Interfaces;

namespace Restaurant.Core.Repositories;

public class TablesRepository : RepositoryWithSave, ITablesRepository
{
    private readonly RestaurantDbContext _dbContext;

    public TablesRepository(RestaurantDbContext dbContext) : base(dbContext)
    {
        
    }

    //public TablesRepository(RestaurantDbContext dbContext)
    //{
    //    _dbContext = dbContext;
    //}

    public async Task<ICollection<Table>> GetAll()
    {
        return await _dbContext.Tables.OrderBy(x => x.Id).ToListAsync();
    }

    public async Task<Table?> Get(int id)
    {
        return await _dbContext.Tables.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> Create(TableForCreateDto dto)
    {
        var obj = new Table
        {
            PriceForHour = dto.PriceForHour
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
        Table? table = await Get(obj.Id);
        if (table is null)
        {
            throw new NullReferenceException("Стіл не знайдений");
        }

        table.PriceForHour = obj.PriceForHour;

        return await Save();
    }

    public async Task<bool> Delete(int id)
    {
        var obj = await Get(id);
        if (obj is null)
        {
            throw new InvalidOperationException("Такий стіл не знайдений.");
        }

        _dbContext.Tables.Remove(obj);
        return await Save();
    }
}