using Mapster;
using Restaurant.Core.DTOs;
using Restaurant.Core.Models;
using Restaurant.Core.Repositories.Interfaces;
using Restaurant.Core.Services.Interfaces;

namespace Restaurant.Core.Services;

public class DishesService : IDishesService
{
    private readonly IDishesRepository _dishesRepository;
    private readonly IDiscountsRepository _discountsRepository;

    public DishesService(IDishesRepository dishesRepository, IDiscountsRepository discountsRepository)
    {
        _dishesRepository = dishesRepository;
        _discountsRepository = discountsRepository;
    }

    public async Task<ICollection<Dish>?> GetAll() => await _dishesRepository.GetAll();
    public async Task<Dish?> Get(int id) => await _dishesRepository.Get(id)?? throw new ArgumentNullException("Такої страви не існує");
    public async Task<bool> Update(Dish obj) => await _dishesRepository.Update(obj);
    public async Task<bool> Delete(int id) => await _dishesRepository.Delete(id);
    public async Task<int> Create(DishForCreateDto dto) => await _dishesRepository.Create(dto);
    public async Task<ICollection<Dish>> GetAvailable() => (await GetAll()).Where(x => x.Available == true).ToList();
    public async Task<ICollection<Dish>> GetDishesOnSale() => (await GetAvailable()).Where(x => x.Discount != null).ToList();

    public async Task<decimal> GetDishResultingPrice(int id)
    {
        Dish dish = await Get(id) ?? throw new ArgumentNullException("Такої страви не існує");
        decimal resultingPrice = dish.Price -
            (decimal)(dish?.Discount?.PecentsAmount ?? 0) * 0.01m * dish.Price
            - (decimal)(dish?.Category?.Discount?.PecentsAmount ?? 0) * dish.Price
            - (decimal)(dish?.Cuisine?.Discount?.PecentsAmount ?? 0) * dish.Price;

        return resultingPrice < 0 ? 0 : resultingPrice;
    }
    
    public async Task<int> AddDiscount(int dishId, double discountSize)
    {
        var dish = await Get(dishId);

        DiscountForCreateDto dto = new DiscountForCreateDto()
        {
            PecentsAmount = discountSize
        };
        int discountId = await _discountsRepository.Create(dto);
        if(discountId < 1)
        {
            throw new InvalidOperationException("Помилка при додаванні знижки");
        }        
        dish.Discount = await _discountsRepository.Get(discountId);
        await _dishesRepository.Update(dish);
        return discountId;
    }
    
}