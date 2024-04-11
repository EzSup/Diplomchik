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
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesService(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public async Task<Guid> Add(Category entity)
            => await _categoriesRepository.Add(entity);

        public async Task<bool> Delete(Guid id)
            => await (_categoriesRepository.Delete(id));

        public async Task<ICollection<Category>> GetAll()
            => await _categoriesRepository.GetAll();

        public async Task<Category> GetById(Guid id)
            => await _categoriesRepository.GetById(id);

        public async Task<ICollection<Category>> GetByPage(int page, int pageSize)
            => await _categoriesRepository.GetByPage(page, pageSize);

        public async Task<int> Purge(IEnumerable<Guid> values)
            => await _categoriesRepository.Purge(values);

        public async Task<bool> Update(Category entity)
            => await _categoriesRepository.Update(entity);
    }
}
