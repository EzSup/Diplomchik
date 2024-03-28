using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Dtos;
using Restaurant.Core.Models;
using Restaurant.Core.Repositories.Interfaces;

namespace Restaurant.Core.Repositories;

public class DiscountsRepository : RepositoryWithSave, IDiscountsRepository
{
    private readonly RestaurantDbContext _dbContext;

    public DiscountsRepository(RestaurantDbContext dbContext) : base(dbContext)
    { _dbContext = dbContext; }
    
    public async Task<ICollection<Discount>> GetAll()
    {
        return await _dbContext.Discounts.Include(d => d.Categories)
            .Include(d => d.Cuisine)
            .Include(d => d.Dishes).OrderBy(x => x.Id).ToListAsync();
    }

    public async Task<Discount?> Get(int id)
    {
        return await _dbContext.Discounts.Include(d => d.Categories)
            .Include(d => d.Cuisine)
            .Include(d => d.Dishes).SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> Create(DiscountForCreateDto dto)
    {
        var obj = new Discount()
        {
            PecentsAmount = dto.PecentsAmount
        };
        _dbContext.Discounts.Add(obj);
        
        if (await Save())
        {
            return obj.Id;
        }

        return 0;
    }

    public async Task<bool> Update(Discount obj)
    {
        Discount? discount = await Get(obj.Id);
        if (discount is null)
        {
            throw new NullReferenceException("Знижка не знайдена");
        }

        discount.PecentsAmount = obj.PecentsAmount;

        return await Save();
    }

    public async Task<bool> Delete(int id)
    {
        var obj = await Get(id);
        if (obj is null)
        {
            throw new InvalidOperationException("Така знижка не знайдена.");
        }

        _dbContext.Discounts.Remove(obj);
        return await Save();
    }
}