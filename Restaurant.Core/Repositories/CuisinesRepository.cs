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
        return await _dbContext.Cuisines.OrderBy(x => x.Name).ToListAsync();
    }

    public async Task<Cuisine?> Get(int id)
    {
        return await _dbContext.Cuisines.SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> Create(CuisineForCreateDto dto)
    {
        var obj = new Cuisine()
        {
            Name = dto.Name,
            DiscountId = dto.DiscountId
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
        Cuisine? cuisine = await Get(obj.Id);
        if (cuisine is null)
        {
            throw new NullReferenceException("Кухня не знайдена");
        }

        cuisine.Name = obj.Name ?? cuisine.Name;
        cuisine.DiscountId = obj.DiscountId;

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