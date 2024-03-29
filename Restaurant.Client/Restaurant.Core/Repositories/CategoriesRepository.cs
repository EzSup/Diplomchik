using Microsoft.EntityFrameworkCore;
using Restaurant.Core.DTOs;
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
            .Include(c => c.Dishes).AsNoTracking().OrderBy(x => x.Name).ToListAsync();
    }

    public async Task<Category?> Get(int id)
    {
        return await _dbContext.Categories.Include(c => c.Discount)
            .Include(c => c.Dishes).AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
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

    public async Task<bool> Update(Category obj)
    {
        await _dbContext.Categories.Where(x => x.Id == obj.Id)
            .ExecuteUpdateAsync(c => c
            .SetProperty(c => c.Name, c => obj.Name)
            .SetProperty(c => c.DiscountId, c => obj.DiscountId));

        return await Save();
    }

    public async Task<bool> Delete(int id)
    {
        await _dbContext.Categories.Where(x => x.Id == id)
            .ExecuteDeleteAsync();
        return await Save();
    }
}