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
        private readonly IDiscountsRepository _discountsRepository;

        public CategoriesService(ICategoriesRepository categoriesRepository,
            IDiscountsRepository discountsRepository)
        {
            _categoriesRepository = categoriesRepository;
            _discountsRepository = discountsRepository;
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

        public async Task<bool> AddDiscount(Guid categoryId, double PercentsAmount)
        {
            var category = await GetById(categoryId);
            if(category == null)
            {
                throw new ArgumentException("Такої категорії не існує!");
            }
            var id = await _discountsRepository.Add(new Discount() { DiscountType = Core.Enums.DiscountType.Category, PecentsAmount = PercentsAmount });
            category.DiscountId = id;
            return await _categoriesRepository.Update(category);
            
            //if(category.DiscountId != null)
            //{
            //    await _discountsRepository.Delete((Guid)category.DiscountId);
            //}
        }

        public async Task<bool> RemoveDiscount(Guid dishId)
        {
            var category = await GetById(dishId);
            Guid discountId = category.DiscountId ?? throw new ArgumentException("Немає знижки в цієї категорії!");
            category.DiscountId = null;
            await Update(category);
            return await _discountsRepository.Delete(discountId);
        }
    }
}
