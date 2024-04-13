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
    public class CartsService : ICartsService
    {
        private readonly ICartsRepository _cartsRepository;
        private readonly IDishCartsRepository _dishCartsRepository;
        private readonly ICustomersRepository _customersRepository;

        public CartsService(ICartsRepository cartsRepository, ICustomersRepository customersRepository, IDishCartsRepository dishCartsRepository)
        {
            _cartsRepository = cartsRepository;
            _customersRepository = customersRepository;
            _dishCartsRepository = dishCartsRepository;
        }

        public async Task<Guid> Add(Cart entity) => await _cartsRepository.Add(entity);

        public async Task<CartResponse> AddDishToUsersCart(Guid customerId, Guid dishId, int count)
        {
            CartResponse cart;
            try
            {
                cart = await _cartsRepository.GetByCustomerId(customerId);
            }
            catch
            {
                var cartId = await _cartsRepository.Add(new Cart());
                cart = await _cartsRepository.GetById(cartId);
            }
            if (!(await _dishCartsRepository.Update(new DishCart() { CartId = cart.Id, DishId = dishId, Count = count })))
            {
                var dishCartId = await _dishCartsRepository.Add(new DishCart() { CartId = cart.Id, DishId = dishId, Count = count });
            }

            return await _cartsRepository.GetByCustomerId(customerId);
        }

        public async Task<CartResponse> GetByCustomerId(Guid CustomerId){

            return await _cartsRepository.GetByCustomerId(CustomerId);
            
        }        

        public async Task<bool> Delete(Guid id) => await _cartsRepository.Delete(id);

        public async Task<CartResponse> GetById(Guid id) => await _cartsRepository.GetById(id);

        public async Task<int> Purge(IEnumerable<Guid> values) => await _cartsRepository.Purge(values);

        public async Task<bool> Update(Cart entity) => await _cartsRepository.Update(entity);
        
        private CartResponse ConvertCartToResponse(Cart cart)
        {
            CartResponse cartResponse = new CartResponse()
            {
                Id = cart.Id,
            };
            foreach (var dish in cart.DishCarts)
            {
                cartResponse.Dishes.Add(new DishOfCart(dish.Dish.Id, dish.Dish.Name, dish.Dish.PhotoLinks.First(), dish.Count));
            }
            return cartResponse;
        }
    }
}
