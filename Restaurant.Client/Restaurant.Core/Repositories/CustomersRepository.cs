using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Dtos;
using Restaurant.Core.Repositories.Interfaces;
using Restaurant.Core.Models;

namespace Restaurant.Core.Repositories;

public class CustomersRepository : RepositoryWithSave, ICustomersRepository
{
    private RestaurantDbContext _dbContext;

    public CustomersRepository(RestaurantDbContext dbContext) : base(dbContext)
    { _dbContext = dbContext; }
    
    public async Task<ICollection<Customer>> GetAll()
    {
        return await _dbContext.Customers.Include(c => c.Bills)
            .Include(c => c.Reviews).AsNoTracking().OrderBy(x => x.Name).ToListAsync();
    }

    public async Task<Customer?> Get(int id)
    {
        return await _dbContext.Customers.Include(c => c.Bills)
            .Include(c => c.Reviews).AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> Create(CustomerForCreateDto dto)
    {
        var obj = new Customer
        {
            Name = dto.Name,
            PhoneNum = dto.PhoneNum,
            Email = dto.Email,
            PhotoLink = dto.PhotoLink,
            Password = dto.Password
        };
        _dbContext.Customers.Add(obj);
        
        if (await Save())
        {
            return obj.Id;
        }

        return 0;
    }

    public async Task<bool> Update(Customer obj)
    {
        await _dbContext.Customers.Where(x => x.Id == obj.Id)
            .ExecuteUpdateAsync(c => c
            .SetProperty(c => c.Name, c=> obj.Name)
            .SetProperty(c => c.PhoneNum, c => obj.PhoneNum)
            .SetProperty(c => c.Email, c => obj.Email)
            .SetProperty(c => c.PhotoLink, c => obj.PhotoLink)
            .SetProperty(c => c.Password, c => obj.Password));

        return await Save();
    }

    public async Task<bool> Delete(int id)
    {
        await _dbContext.Customers.Where(x => x.Id == id).ExecuteDeleteAsync();

        return await Save();
    }
}