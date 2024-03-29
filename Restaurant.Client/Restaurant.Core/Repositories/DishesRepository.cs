using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Dtos;
using Restaurant.Core.Models;
using Restaurant.Core.Repositories.Interfaces;

namespace Restaurant.Core.Repositories;

public class DishesRepository : RepositoryWithSave, IDishesRepository
{
    private readonly RestaurantDbContext _dbContext;

    public DishesRepository(RestaurantDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<ICollection<Dish>> GetAll()
    {
        return await _dbContext.Dishes.Include(d => d.Discount)
            .Include(d => d.Cuisine)
            .Include(d => d.Category)
            .OrderBy(x => x.Name).AsNoTracking().ToListAsync();
    }

    public async Task<Dish?> Get(int id)
    {
        return await _dbContext.Dishes.Include(d => d.Discount)
            .Include(d => d.Cuisine)
            .Include(d => d.Category)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> Create(DishForCreateDto dto)
    {
        var obj = new Dish()
        {
            Name = dto.Name,
            Weight = dto.Weight,
            Available = dto.Available,
            Price = dto.Price,
            IngredientsList = dto.IngredientsList,
            PhotoLinks = dto.PhotoLinks,
            DiscountId = dto. DiscountId is null || dto.DiscountId < 1 ? null : dto.DiscountId,
            CategoryId = dto.CategoryId is null || dto.CategoryId < 1 ? null : dto.CategoryId,
            CuisineId = dto.CuisineId is null || dto.CuisineId < 1 ? null : dto.CuisineId
        };
        _dbContext.Dishes.Add(obj);
        
        if (await Save())
        {
            return obj.Id;
        }

        return 0;
    }

    public async Task<bool> Update(Dish obj)
    {
        await _dbContext.Dishes.Where(x => x.Id == obj.Id)
            .ExecuteUpdateAsync(d => d
            .SetProperty(d => d.Name, d => obj.Name)
            .SetProperty(d => d.Weight, d => obj.Weight)
            .SetProperty(d => d.Available, d => obj.Available)
            .SetProperty(d => d.Price, d=> obj.Price)
            .SetProperty(d => d.IngredientsList, d => obj.IngredientsList)
            .SetProperty(d => d.PhotoLinks, d => obj.PhotoLinks)
            .SetProperty(d => d.DiscountId, d => obj.DiscountId)
            .SetProperty(d => d.CategoryId, d => obj.CategoryId)
            .SetProperty(d => d.CuisineId, d => obj.CuisineId));

        return await Save();
    }

    public async Task<bool> Delete(int id)
    {
        await _dbContext.Dishes.Where(d => d.Id == id).ExecuteDeleteAsync();
        return await Save();
    }
}