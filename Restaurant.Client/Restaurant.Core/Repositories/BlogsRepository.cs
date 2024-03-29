using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Dtos;
using Restaurant.Core.DTOs.ForCreate;
using Restaurant.Core.Models;
using Restaurant.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Repositories
{
    public class BlogsRepository : RepositoryWithSave, IBlogsRepository
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly DateTime minDate = new DateTime(2024, 4, 1);

        public BlogsRepository(RestaurantDbContext dbContext) : base(dbContext)
        { _dbContext = dbContext; }

        public async Task<ICollection<Blog>> GetAll()
        {
            return await _dbContext.Blogs.AsNoTracking().OrderBy(x => x.Created).ToListAsync();
        }

        public async Task<Blog?> Get(int id)
        {
            return await _dbContext.Blogs.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<int> Create(BlogForCreateDto dto)
        {
            var obj = new Blog()
            {
                AuthorName = dto.AuthorName,
                Content = dto.Content,
                Title = dto.Title,
                Created = DateTime.Now
            };
            _dbContext.Blogs.Add(obj);

            if (await Save())
            {
                return obj.Id;
            }

            return 0;
        }

        public async Task<bool> Update(Blog obj)
        {
            await _dbContext.Blogs.Where(x => x.Id == obj.Id)
                .ExecuteUpdateAsync(b => b
                .SetProperty(b => b.Title, b => obj.Title)
                .SetProperty(b => b.Content, b => obj.Content)
                .SetProperty(b => b.AuthorName, b => obj.AuthorName)
                .SetProperty(b => b.Created, b => obj.Created > minDate ? obj.Created : b.Created));

            return await Save();
        }

        public async Task<bool> Delete(int id)
        {
            await _dbContext.Blogs.Where(x => x.Id == id)
                .ExecuteDeleteAsync();  
            return await Save();
        }
    }
}
