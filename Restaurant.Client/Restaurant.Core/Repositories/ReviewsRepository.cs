using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Dtos;
using Restaurant.Core.Models;
using Restaurant.Core.Repositories.Interfaces;

namespace Restaurant.Core.Repositories;

public class ReviewsRepository : RepositoryWithSave, IReviewsRepository
{
    private readonly RestaurantDbContext _dbContext;

    public ReviewsRepository(RestaurantDbContext dbContext) : base(dbContext)
    { _dbContext = dbContext; }
    
    public async Task<ICollection<Review>> GetAll()
    {
        return await _dbContext.Reviews.Include(r => r.Author)
            .Include(r => r.Dish)
            .OrderBy(x => x.Title).AsNoTracking().ToListAsync();
    }

    public async Task<Review?> Get(int id)
    {
        return await _dbContext.Reviews.Include(r => r.Author)
            .Include(r => r.Dish).AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
    }

    public async Task<int> Create(ReviewForCreateDto dto)
    {
        var obj = new Review()
        {
            Title = dto.Title,
            Content = dto.Content,
            Rate = dto.Rate,
            AuthorId = dto.AuthorId is null || dto.AuthorId < 1 ? null : dto.AuthorId,
            DishId = dto.DishId is null || dto.DishId < 1 ? null : dto.DishId,
        };
        _dbContext.Reviews.Add(obj);
        
        if (await Save())
        {
            return obj.Id;
        }

        return 0;
    }

    public async Task<bool> Update(Review obj)
    {
        await _dbContext.Reviews.Where(x => x.Id == obj.Id)
            .ExecuteUpdateAsync(r => r
            .SetProperty(r => r.Title, r => obj.Title)
            .SetProperty(r => r.Content, r => obj.Content)
            .SetProperty(r => r.Rate, r => obj.Rate)
            .SetProperty(r => r.AuthorId, r => obj.AuthorId)
            .SetProperty(r => r.DishId, r => obj.DishId));
        return await Save();
    }

    public async Task<bool> Delete(int id)
    {
        await _dbContext.Reviews.Where(x => x.Id == id)
            .ExecuteDeleteAsync();

        return await Save();
    }
}