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
        return await _dbContext.Customers.OrderBy(x => x.Name).ToListAsync();
    }

    public async Task<Customer?> Get(int id)
    {
        return await _dbContext.Customers.SingleOrDefaultAsync(x => x.Id == id);
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
        Customer? customer = await Get(obj.Id);
        if (customer is null)
        {
            throw new NullReferenceException("Користувач не знайдений");
        }

        customer.Name = obj.Name ?? customer.Name;
        customer.PhoneNum = obj.PhoneNum ?? customer.PhoneNum;
        customer.Email = obj.Email ?? customer.Email;
        customer.PhotoLink = obj.PhotoLink ?? customer.PhotoLink;
        customer.Password = obj.Password ?? customer.Password;
        return await Save();
    }

    public async Task<bool> Delete(int id)
    {
        var obj = await Get(id);
        if (obj is null)
        {
            throw new InvalidOperationException("Такий користувач не знайдений.");
        }

        _dbContext.Customers.Remove(obj);
        return await Save();
    }
}