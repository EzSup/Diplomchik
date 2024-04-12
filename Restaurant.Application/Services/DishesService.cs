using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Restaurant.Application.Interfaces.Services;
using Restaurant.Core.Dtos;
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

        public DishesService(IDishesRepository dishesRepository)
        {
            _dishesRepository = dishesRepository;
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

        public async Task<int> PagesCount(int pageSize, string? Name = null,
            double MinWeight = 0,
            double MaxWeight = double.MaxValue,
            IEnumerable<string>? Ingredients = null,
            bool? Available = null,
            decimal MinPrice = 0,
            decimal MaxPrice = decimal.MaxValue,
            string? Category = null,
            string? Cuisine = null,
            double discountPercentsMin = 0)
        {
            var dishes = await _dishesRepository.GetByFilter(Name, MinWeight, MaxWeight,
                Ingredients, Available, MinPrice, MaxPrice,
                Category, Cuisine, discountPercentsMin);
            if (dishes.Count() == 0)
                return 0;
            return dishes.Count()/pageSize + 1;
        }

        public async Task<ICollection<DishPaginationResponse>> GetByFilter(string? Name = null,
            double MinWeight = 0,
            double MaxWeight = double.MaxValue,
            IEnumerable<string>? Ingredients = null,
            bool? Available = null,
            decimal MinPrice = 0,
            decimal MaxPrice = decimal.MaxValue,
            string? Category = null,
            string? Cuisine = null,
            double discountPercentsMin = 0)
            => await _dishesRepository.GetByFilter(Name, MinWeight, MaxWeight,
                Ingredients, Available, MinPrice, MaxPrice,
                Category, Cuisine, discountPercentsMin);

        public async Task<ICollection<Dish>> GetByPage(int page, int pageSize)
            => await _dishesRepository.GetByPage(page, pageSize);
        
        public async Task<ICollection<Dish>> GetByPageAvailable(int page, int pageSize)
            => await _dishesRepository.GetByPageAvailable(page, pageSize);

        public async Task<ICollection<DishPaginationResponse>> GetByFilterPage(int page, int pageSize, string? Name = null,
            double MinWeight = 0,
            double MaxWeight = double.MaxValue,
            IEnumerable<string>? Ingredients = null,
            bool? Available = null,
            decimal MinPrice = 0,
            decimal MaxPrice = decimal.MaxValue,
            string? Category = null,
            string? Cuisine = null,
            double discountPercentsMin = 0)
        {
            var filtered = await _dishesRepository.GetByFilter(Name, MinWeight, MaxWeight,
                Ingredients, Available, MinPrice, MaxPrice,
                Category, Cuisine, discountPercentsMin);

            if (pageSize > 0 && page > 0)
            {
                return filtered
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
            }
            throw new ArgumentException("Page size and number has to be greater than 0!");
        }
    }
}
