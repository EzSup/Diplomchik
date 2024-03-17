using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Dtos;
using Restaurant.Core.Models;
using Restaurant.Core.Repositories.Interfaces;

namespace Restaurant.Core.Repositories;

public class CategoriesRepository : RepositoryWithSave, ICategoriesRepository
{
    private readonly RestaurantDbContext _dbContext;

    public CategoriesRepository(RestaurantDbContext dbContext) : base(dbContext)
    { _dbContext = dbContext; }
    
    public async Task<ICollection<Category>> GetAll()
    {
        return await _dbContext.Categories.Include(c => c.Discount)
            .Include(c => c.Dishes).OrderBy(x => x.Name).ToListAsync();
    }

    public async Task<Category?> Get(int id)
    {
        return await _dbContext.Categories.Include(c => c.Discount)
            .Include(c => c.Dishes).SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> Create(CategoryForCreateDto dto)
    {
        var obj = new Category()
        {
            Name = dto.Name,
            DiscountId = dto.DiscountId is null || dto.DiscountId < 1 ? null : dto.DiscountId
        };
        _dbContext.Categories.Add(obj);
        
        if (await Save())
        {
            return obj.Id;
        }

        return 0;
    }

    public async Task<bool> Update(CategoryDto obj)
    {
        Category? category = await Get(obj.Id);
        if (category is null)
        {
            throw new NullReferenceException("Категорія не знайдена");
        }

        category.Name = obj.Name ?? category.Name;
        category.DiscountId = obj.DiscountId is null || obj.DiscountId < 1 ? null : obj.DiscountId;

        return await Save();
    }

    public async Task<bool> Delete(int id)
    {
        var obj = await Get(id);
        if (obj is null)
        {
            throw new InvalidOperationException("Така категорія не знайдена.");
        }

        _dbContext.Categories.Remove(obj);
        return await Save();
    }
}