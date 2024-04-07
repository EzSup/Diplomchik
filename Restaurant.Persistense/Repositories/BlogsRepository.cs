using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Interfaces;
using Restaurant.Core.Models;

namespace Restaurant.Persistense.Repositories
{
    public class BlogsRepository : IBlogsRepository
    {
        private readonly DateTime minDate = new DateTime(2024, 1, 1);
        private readonly RestaurantDbContext _context;

        public BlogsRepository(RestaurantDbContext restaurantDbContext)
        {
            _context = restaurantDbContext;
        }

        public async Task<ICollection<Blog>> GetAll()
        {
            return await _context.Blogs
                .AsNoTracking()
                .OrderBy(t => t.Created)
                .ToListAsync();
        }

        public async Task<Blog> GetById(Guid id)
        {
            return await _context.Blogs
                .AsNoTracking()
                .FirstOrDefaultAsync(t => t.Id == id) ?? throw new KeyNotFoundException("Blog not found!");
        }

        public async Task<ICollection<Blog>> GetByFilter(DateTime after, string? AuthorName, string? TitleContains)
        {
            var query = _context.Blogs.AsNoTracking();

            if (!String.IsNullOrWhiteSpace(AuthorName))
            {
                query = query.Where(t => t.AuthorName.Contains(AuthorName));
            }
            if (!String.IsNullOrWhiteSpace(TitleContains))
            {
                query = query.Where(t => t.Title.Contains(TitleContains));
            }
            if (after >= minDate)
            {
                query = query.Where(t => t.Created > after);
            }

            return await query.ToListAsync();
        }

        public async Task<ICollection<Blog>> GetByPage(int page, int pageSize)
        {
            if (pageSize > 0 && page > 0)
            {
                return await _context.Blogs.AsNoTracking()
                    .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            throw new ArgumentException("Page size and number has to be greater than 0!");
        }

        public async Task<Guid> Add(Blog obj)
        {
            var blog = new Blog(obj.AuthorName, obj.Title, obj.Content);
            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();
            return blog.Id;
        }

        public async Task<bool> Update(Blog obj)
        {
            return await _context.Blogs
                .Where(t => t.Id == obj.Id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(t => t.AuthorName, obj.AuthorName)
                    .SetProperty(t => t.Title, obj.Title)
                    .SetProperty(t => t.Content, obj.Content)) == 1;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _context.Blogs
                .Where(t => t.Id == id)
                .ExecuteDeleteAsync() == 1;
        }

        public async Task<int> Purge(IEnumerable<Guid> ids)
        {
            return await _context.Blogs
                .Where(t => ids.Contains(t.Id))
                .ExecuteDeleteAsync();
        }
    }
}
