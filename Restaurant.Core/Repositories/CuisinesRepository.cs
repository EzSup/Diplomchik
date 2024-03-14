using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Dtos;
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
            .Include(c => c.Dishes).OrderBy(x => x.Name).ToListAsync();
    }

    public async Task<Cuisine?> Get(int id)
    {
        return await _dbContext.Cuisines.Include(c => c.Discount)
            .Include(c => c.Dishes).SingleOrDefaultAsync(x => x.Id == id);
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

    public async Task<bool> Update(CuisineDto obj)
    {
        Cuisine? cuisine = await Get(obj.Id);
        if (cuisine is null)
        {
            throw new NullReferenceException("Кухня не знайдена");
        }

        cuisine.Name = obj.Name ?? cuisine.Name;
        cuisine.DiscountId = obj.DiscountId is null || obj.DiscountId < 1 ? null : obj.DiscountId;

        return await Save();
    }

    public async Task<bool> Delete(int id)
    {
        var obj = await Get(id);
        if (obj is null)
        {
            throw new InvalidOperationException("Така кухня не знайдена.");
        }

        _dbContext.Cuisines.Remove(obj);
        return await Save();
    }
}