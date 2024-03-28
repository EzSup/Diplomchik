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
            .OrderBy(x => x.Title).ToListAsync();
    }

    public async Task<Review?> Get(int id)
    {
        return await _dbContext.Reviews.Include(r => r.Author)
            .Include(r => r.Dish).SingleOrDefaultAsync(x => x.Id == id);
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
        Review? review = await Get(obj.Id);
        if (review is null)
        {
            throw new NullReferenceException("Відгук не знайдений");
        }

        review.Title = obj.Title;
        review.Content = obj.Content ?? review.Content;
        review.Rate = obj.Rate ?? review.Rate;
        review.AuthorId = obj.AuthorId is null || obj.AuthorId < 1 ? null : obj.AuthorId;
        review.DishId = obj.DishId is null || obj.DishId < 1 ? null : obj.DishId;

        return await Save();
    }

    public async Task<bool> Delete(int id)
    {
        var obj = await Get(id);
        if (obj is null)
        {
            throw new InvalidOperationException("Така категорія не знайдена.");
        }

        _dbContext.Reviews.Remove(obj);
        return await Save();
    }
}