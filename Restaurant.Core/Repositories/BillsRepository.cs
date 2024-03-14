using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Dtos;
using Restaurant.Core.Models;
using Restaurant.Core.Repositories.Interfaces;

namespace Restaurant.Core.Repositories;

public class BillsRepository : RepositoryWithSave, IBillsRepository
{
    private readonly RestaurantDbContext _dbContext;

    public BillsRepository(RestaurantDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ICollection<Bill>> GetAll()
    {
        return await _dbContext.Bills.Include(b=>b.Table)
            .Include(b => b.Customer)
            .Include(b => b.DishBills)
            .OrderBy(x => x.Id).ToListAsync();
    }

    public async Task<Bill?> Get(int id)
    {
        return await _dbContext.Bills.SingleOrDefaultAsync(x => x.Id == id);
    }
    
    private ICollection<DishBill> MapDishBills(int billId, IDictionary<string, int> dishbills)
    {
        List<DishBill> dishBillsOFThisBill = new();
        foreach (var dishbill in dishbills)
        {
            DishBill newDishBill = new()
            {
                BillId = billId,
                DishId = Convert.ToInt32(dishbill.Key),
                DishesCount = dishbill.Value
            };
            _dbContext.DishBills.Add(newDishBill);
            dishBillsOFThisBill.Add(newDishBill);
        }
        return dishBillsOFThisBill;
    }

    public async Task<int> Create(BillForCreateDto dto)
    {
        var obj = new Bill()
        {
            OrderDateAndTime = DateTime.Now,
            PaidAmount = dto.PaidAmount,
            TipsPercents = dto.TipsPercents,
            TableId = dto.TableId is null || dto.TableId < 1 ? null : dto.TableId,
            CustomerId = dto.CustomerId is null || dto.CustomerId < 1 ? null : dto.CustomerId
        };
        _dbContext.Bills.Add(obj);
        obj.DishBills = MapDishBills(obj.Id, dto.DishesAndCount);
        if (await Save())
        {
            return obj.Id;
        }

        return 0;
    }

    public async Task<bool> Update(Bill obj)
    {
        Bill? bill = await Get(obj.Id);
        if (bill is null)
        {
            throw new NullReferenceException("Чек не знайдений");
        }

        bill.OrderDateAndTime = obj.OrderDateAndTime;
        bill.PaidAmount = obj.PaidAmount;
        bill.TipsPercents = obj.TipsPercents;
        bill.TableId = obj.TableId is null || obj.TableId < 1 ? null : obj.TableId;
        bill.CustomerId = obj.CustomerId is null || obj.CustomerId < 1 ? null : obj.CustomerId;

        return await Save();
    }

    public async Task<bool> Delete(int id)
    {
        var obj = await Get(id);
        if (obj is null)
        {
            throw new InvalidOperationException("Такий чек не знайдений.");
        }

        _dbContext.Bills.Remove(obj);
        return await Save();
    }
}