using Restaurant.Core.Dtos;
using Restaurant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Interfaces.Services
{
    public interface ICartsService
    {
        Task<CartResponse> GetById(Guid id);
        Task<Guid> Add(Cart entity);
        Task<bool> Update(Cart entity);
        Task<bool> Delete(Guid id);
        Task<int> Purge(IEnumerable<Guid> values);
        Task<CartResponse> AddDishToUsersCart(Guid userId, Guid dishId, int count);
        Task<CartResponse> GetByCustomerId(Guid CustomerId);
    }
}
