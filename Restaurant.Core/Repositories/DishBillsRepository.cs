using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Dtos;
using Restaurant.Core.Models;
using Restaurant.Core.Repositories.Interfaces;

namespace Restaurant.Core.Repositories;

public class DishBillsRepository : RepositoryWithSave, IDishBillsRepository
{
    private readonly RestaurantDbContext _dbContext;

    public DishBillsRepository(RestaurantDbContext dbContext) : base(dbContext)
    { _dbContext = dbContext; }
    
    public async Task<ICollection<DishBill>> GetAll()
    {
        return await _dbContext.DishBills.OrderBy(x => x.BillId).ToListAsync();
    }

    public async Task<DishBill?> Get(int id)
    {
        return await _dbContext.DishBills.SingleOrDefaultAsync(x => x.BillId == id);
    }

    public async Task<int> Create(DishBillForCreateDto dto)
    {
        var obj = new DishBill()
        {
            DishId = dto.DishId,
            BillId = dto.BillId,
            DishesCount = dto.DishesCount
        };
        _dbContext.DishBills.Add(obj);
        
        if (await Save())
        {
            return obj.BillId;
        }

        return 0;
    }

    public async Task<bool> Update(DishBill obj)
    {
        DishBill? dishBill = await Get(obj.BillId);
        if (dishBill is null)
        {
            throw new NullReferenceException("DishBill не знайдена");
        }

        dishBill.DishId = obj.DishId;
        dishBill.BillId = obj.BillId;
        dishBill.DishesCount = obj.DishesCount;

        return await Save();
    }

    public async Task<bool> Delete(int id)
    {
        var obj = await Get(id);
        if (obj is null)
        {
            throw new InvalidOperationException("Така DishBill не знайдена.");
        }

        _dbContext.DishBills.Remove(obj);
        return await Save();
    }
}