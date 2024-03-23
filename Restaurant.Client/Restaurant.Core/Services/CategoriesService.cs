using Restaurant.Core.Dtos;
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
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _repository;

        public CategoriesService( ICategoriesRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<Category>> GetAll() => await _repository.GetAll();
        public async Task<Category?> Get(int id) => await _repository.Get(id);
        public async Task<bool> Update(CategoryDto dto) => await _repository.Update(dto);
        public async Task<int> Create(CategoryForCreateDto dto) => await _repository.Create(dto);
        public async Task<bool> Delete(int id) => await _repository.Delete(id);

        public async Task BindDishes(int categoryId, IEnumerable<Dish> dishes)
        {
            var category = await _repository.Get(categoryId);
            if (category == null) { throw new ArgumentException("Не існує такої категорії"); }
            foreach (var dish in dishes)
            {
                dish.CategoryId = categoryId;
            }
        }

        public async Task UnbindDishes(int categoryId, IEnumerable<Dish> dishes)
        {
            var category = await _repository.Get(categoryId);
            if (category == null) { throw new ArgumentException("Не існує такої категорії"); }
            foreach (var dish in dishes)
            {
                dish.CategoryId = categoryId;
            }
        }
    }
}
