using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Interfaces;
using Restaurant.Core.Models;

namespace Restaurant.Persistense.Repositories
{
    public class ReviewsRepository : IReviewsRepository
    {
        private readonly RestaurantDbContext _context;

        public ReviewsRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Add(Review entity)
        {
            var model = new Review()
            {
                AuthorId = entity.AuthorId,
                DishId = entity.DishId,
                Title = entity.Title,
                Rate = entity.Rate,                
                Content = entity.Content
            };
            await _context.Reviews.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.Id;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _context.Reviews
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync() == 1;
        }

        public async Task<ICollection<Review>> GetAll()
        {
            return await _context.Reviews
                .AsNoTracking()
                .OrderByDescending(x => x.Posted)
                .ToListAsync();
        }

        public async Task<Review> GetById(Guid id)
        {
            return await _context.Reviews.AsNoTracking()
                .Include(x => x.Dish)
                .Include(x => x.Author)
                .FirstOrDefaultAsync(x => x.Id == id) ?? throw new KeyNotFoundException("Review not found!");
        }

        public async Task<ICollection<Review>> GetByPage(int page, int pageSize)
        {
            if (pageSize > 0 && page > 0)
            {
                return await _context.Reviews.AsNoTracking()
                    .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            throw new ArgumentException("Page size and number has to be greater than 0!");
        }

        public async Task<ICollection<Review>> GetByFilter(Guid? DishId = null, Guid? AuthorId = null, double minRate = 1, double maxRate = 5)
        {
            var query = _context.Reviews.AsNoTracking();
            if(DishId != null)
            {
                query = query.Where(x => x.DishId == DishId);
            }
            if(AuthorId != null)
            {
                query = query.Where(x => x.AuthorId == AuthorId);
            }
            if(minRate <= maxRate && minRate > 0 && maxRate <= 5)
            {
                query = query.Where(x => x.Rate > minRate && x.Rate < maxRate);
            }
            return await query.ToListAsync();
        }

        public async Task<int> Purge(IEnumerable<Guid> values)
        {
            return await _context.Reviews
                .Where(t => values.Contains(t.Id))
                .ExecuteDeleteAsync();
        }

        public async Task<bool> Update(Review entity)
        {
            return await _context.Reviews
                .Where(r => r.Id == entity.Id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(r => r.Title, entity.Title)
                    .SetProperty(r => r.Content, entity.Content)
                    .SetProperty(r => r.Rate, entity.Rate)) == 1;
        }
    }
}
