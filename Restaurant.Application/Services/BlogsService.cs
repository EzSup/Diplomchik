using Restaurant.Application.Interfaces.Repositories;
using Restaurant.Application.Interfaces.Services;
using Restaurant.Core.Interfaces;
using Restaurant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Services
{
    public class BlogsService : IBlogsService
    {
        private readonly IBlogsRepository _blogsRepository;

        public BlogsService(IBlogsRepository blogsRepository)
        {
            _blogsRepository = blogsRepository;
        }

        public async Task<ICollection<Blog>> GetAll() => await _blogsRepository.GetAll();
        public async Task<Blog> GetById(Guid Id) => await _blogsRepository.GetById(Id);
        public async Task<ICollection<Blog>> GetByPage(int page, int pageSize) => await _blogsRepository.GetByPage(page, pageSize);
        public async Task<ICollection<Blog>> GetByFilter(DateTime created, string? authorName, string? titleContains) 
            => await _blogsRepository.GetByFilter(created, authorName, titleContains);

        public async Task<Guid> Add(Blog obj) => await _blogsRepository.Add(obj);
        public async Task<bool> Update(Blog obj) => await _blogsRepository.Update(obj);
        public async Task<bool> Delete(Guid id) => await _blogsRepository.Delete(id);
        public async Task<int> Purge(IEnumerable<Guid> ids) => await _blogsRepository.Purge(ids);
    }
}
