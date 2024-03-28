using Restaurant.Core.Dtos;
using Restaurant.Core.DTOs.ForCreate;
using Restaurant.Core.Models;
using Restaurant.Core.Repositories.Interfaces;
using Restaurant.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Services
{
    public class BlogsService : IBlogsService
    {
        private IBlogsRepository _repository;
        public BlogsService(IBlogsRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<Blog>> GetAll() => await _repository.GetAll();
        public async Task<Blog?> Get(int id) => await _repository.Get(id);
        public async Task<bool> Update(Blog dto) => await _repository.Update(dto);
        public async Task<int> Create(BlogForCreateDto dto) => await _repository.Create(dto);
        public async Task<bool> Delete(int id) => await _repository.Delete(id);
    }
}
