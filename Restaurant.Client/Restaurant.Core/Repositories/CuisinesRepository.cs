using Microsoft.EntityFrameworkCore;
using Restaurant.Core.DTOs;
using Restaurant.Core.Models;
using Restaurant.Core.Repositories.Interfaces;

namespace Restaurant.Core.Repositories;

public class CuisinesRepository : RepositoryWithSave , ICuisinesRepository
{
    private readonly RestaurantDbContext _dbContext;

    public CuisinesRepository(RestaurantDbContext dbContext) : base(dbContext)
    { _dbContext = dbContext; }
    
    public async Task<ICollection<Cuisine>> GetAll()
    {
        return await _dbContext.Cuisines.Include(c => c.Discount)
            .Include(c => c.Dishes).AsNoTracking().OrderBy(x => x.Name).ToListAsync();
    }

    public async Task<Cuisine?> Get(int id)
    {
        return await _dbContext.Cuisines.Include(c => c.Discount)
            .Include(c => c.Dishes).AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> Create(CuisineForCreateDto dto)
    {
        var obj = new Cuisine()
        {
            Name = dto.Name,
            DiscountId = dto.DiscountId is null || dto.DiscountId < 1 ? null : dto.DiscountId
        };
        _dbContext.Cuisines.Add(obj);
        
        if (await Save())
        {
            return obj.Id;
        }

        return 0;
    }

    public async Task<bool> Update(Cuisine obj)
    {
        await _dbContext.Cuisines.Where(x => x.Id == obj.Id)
            .ExecuteUpdateAsync(c => c
            .SetProperty(c => c.Name, c => obj.Name)
            .SetProperty(c => c.DiscountId, c=> obj.DiscountId));

        return await Save();
    }

    public async Task<bool> Delete(int id)
    {
        await _dbContext.Cuisines.Where(x => x.Id == id).ExecuteDeleteAsync();
            
        return await Save();
    }
}