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
            return await _dbContext.Blogs.OrderBy(x => x.Created).ToListAsync();
        }

        public async Task<Blog?> Get(int id)
        {
            return await _dbContext.Blogs.SingleOrDefaultAsync(x => x.Id == id);
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
            Blog? blog = await Get(obj.Id);
            if (blog is null)
            {
                throw new NullReferenceException("Блог не знайдений");
            }

            blog.Title = obj.Title ?? blog.Title;
            blog.Content = obj.Content ?? blog.Content;
            blog.AuthorName = obj.AuthorName ?? blog.AuthorName;
            blog.Created = obj.Created < minDate ? blog.Created : obj.Created;

            return await Save();
        }

        public async Task<bool> Delete(int id)
        {
            var obj = await Get(id);
            if (obj is null)
            {
                throw new InvalidOperationException("Такий блог не знайдений.");
            }

            _dbContext.Blogs.Remove(obj);
            return await Save();
        }
    }
}
