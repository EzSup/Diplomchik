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
    public class CustomersService : ICustomersService
    {
        private readonly ICustomersRepository _customersRepository;

        public CustomersService(ICustomersRepository customersRepository)
        {
            _customersRepository = customersRepository;
        }

        public async Task<Guid> Add(Customer entity)
        {
            return await _customersRepository.Add(entity);
        }

        public async Task<bool> UpdateImage(Guid id, string ImageLink)
        {
            var customer = await _customersRepository.GetById(id);
            customer.PhotoLink = ImageLink;
            return await _customersRepository.Update(customer);
        }

        public async Task<bool> Delete(Guid id) => await _customersRepository.Delete(id);

        public async Task<ICollection<Customer>> GetAll()=> await _customersRepository.GetAll();

        public async Task<Customer> GetById(Guid id) => await _customersRepository.GetById(id);

        public async Task<int> Purge(IEnumerable<Guid> values) => await _customersRepository.Purge(values);

        public async Task<bool> Update(Customer entity) => await _customersRepository.Update(entity);

        public async Task<Customer> GetByUser(Guid id) => await _customersRepository.GetByUser(id);
    }
}
