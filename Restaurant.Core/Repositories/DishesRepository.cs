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
        return await _dbContext.Dishes.OrderBy(x => x.Name).ToListAsync();
    }

    public async Task<Dish?> Get(int id)
    {
        return await _dbContext.Dishes.SingleOrDefaultAsync(x => x.Id == id);
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
        Dish? dish = await Get(obj.Id);
        if (dish is null)
        {
            throw new NullReferenceException("Страва не знайдена");
        }

        dish.Name = obj.Name;
        dish.Weight = obj.Weight;
        dish.Available = obj.Available;
        dish.Price = obj.Price;
        dish.IngredientsList = obj.IngredientsList;
        dish.PhotoLinks = obj.PhotoLinks;
        dish.DiscountId = obj.DiscountId is null || obj.DiscountId < 1 ? null : obj.DiscountId;
        dish.CategoryId = obj.CategoryId is null || obj.CategoryId < 1 ? null : obj.CategoryId;
        dish.CuisineId = obj.CuisineId is null || obj.CuisineId < 1 ? null : obj.CategoryId;

        return await Save();
    }

    public async Task<bool> Delete(int id)
    {
        var obj = await Get(id);
        if (obj is null)
        {
            throw new InvalidOperationException("Така страва не знайдена.");
        }

        _dbContext.Dishes.Remove(obj);
        return await Save();
    }
}