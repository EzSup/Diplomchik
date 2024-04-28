using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Restaurant.Application.Interfaces.Services;
using Restaurant.Core.Dtos.Dish;
using Restaurant.Core.Enums;
using Restaurant.Core.Interfaces;
using Restaurant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Services
{
    public class DishesService : IDishesService
    {
        private readonly IDishesRepository _dishesRepository;
        private readonly IDiscountsRepository _discountsRepository;

        public DishesService(IDishesRepository dishesRepository, IDiscountsRepository discountsRepository)
        {
            _dishesRepository = dishesRepository;
            _discountsRepository = discountsRepository;
        }

        public async Task<ICollection<Dish>> GetAll() 
            => await _dishesRepository.GetAll();
        public async Task<Dish> GetById(Guid id)
            => await _dishesRepository.GetById(id);
        public async Task<Guid> Add(Dish entity)
            => await _dishesRepository.Add(entity);
        public async Task<bool> Update(Dish entity)
            => await (_dishesRepository.Update(entity));
        public async Task<bool> Delete(Guid id)
            => await _dishesRepository.Delete(id);
        public async Task<int> Purge(IEnumerable<Guid> values)
            => await _dishesRepository.Purge(values);

        public async Task<int> PagesCount(DishPaginationRequest request)
        {
            var dishes = await _dishesRepository.GetByFilter(DishSortingOrder.Name, request.Name, request.MinWeight, request.MaxWeight,
                request.Ingredients, request.Available, request.MinPrice, request.MaxPrice,
                request.Category, request.Cuisine, request.MinDiscountsPercents);
            if (dishes.Count() == 0)
                return 0;
            return dishes.Count()/request.pageSize + 1;
        }

        //public async Task<ICollection<DishPaginationResponse>> GetByFilter(DishPaginationRequest request)
        //    => await _dishesRepository.GetByFilter(request.order, request.Name, request.MinWeight, request.MaxWeight,
        //        request.Ingredients, request.Available, request.MinPrice, request.MaxPrice,
        //        request.Category, request.Cuisine, request.MinDiscountsPercents);

        public async Task<ICollection<Dish>> GetByPage(int page, int pageSize)
            => await _dishesRepository.GetByPage(page, pageSize);
        
        public async Task<ICollection<Dish>> GetByPageAvailable(int page, int pageSize)
            => await _dishesRepository.GetByPageAvailable(page, pageSize);

        public async Task<ICollection<DishPaginationResponse>> GetByFilter(DishPaginationRequest request)
        {
            var filtered = await _dishesRepository.GetByFilter(request.order, request.Name, request.MinWeight, request.MaxWeight,
                request.Ingredients, request.Available, request.MinPrice, request.MaxPrice,
                request.Category, request.Cuisine, request.MinDiscountsPercents);

            if (request.pageSize > 0 && request.pageIndex > 0)
            {
                return filtered
                    .Skip((request.pageIndex - 1) * request.pageSize)
                    .Take(request.pageSize)
                    .ToList();
            }
            throw new ArgumentException("Page size and number has to be greater than 0!");
        }

        public async Task<bool> AddDiscount(Guid dishId, double PercentsAmount)
        {
            var dish = await GetById(dishId);
            if (dish == null)
            {
                throw new ArgumentException("Такої страви не існує!");
            }
            var id = await _discountsRepository.Add(new Discount() { DiscountType = Core.Enums.DiscountType.Dish, PecentsAmount = PercentsAmount });
            dish.DiscountId = id;
            return await _dishesRepository.Update(dish);

        }

        public async Task<bool> RemoveDiscount(Guid dishId)
        {
            var dish = await GetById(dishId);
            Guid discountId = dish.DiscountId ?? throw new ArgumentException("Немає знижки в цієї страви!");            
            dish.DiscountId = null;
            await Update(dish);
            return await _discountsRepository.Delete(discountId);
        }
    }
}
