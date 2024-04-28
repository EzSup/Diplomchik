using Restaurant.Application.Interfaces.Services;
using Restaurant.Core.Dtos.Customer;
using Restaurant.Core.Interfaces;
using Restaurant.Core.Models;

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
        
        public async Task<Customer> GetByUser(Guid id) => await _customersRepository.GetByUser(id);

        public async Task<int> Purge(IEnumerable<Guid> values) => await _customersRepository.Purge(values);

        public async Task<bool> Update(Customer entity) => await _customersRepository.Update(entity);

        public async Task<ICollection<CustomerResponse>> GetAllAsResponses() 
        {
            var customers = await _customersRepository.GetAll();
            List<CustomerResponse> response = new();
            foreach (var customer in customers)
            {
                response.Add(CustomerToResponse(customer));
            }
            return response;
        }

        public async Task<CustomerResponse> GetResponseById(Guid id)
        {
            var customer = await _customersRepository.GetById(id);
            var resposne = CustomerToResponse(customer);
            return resposne;
        }

        public async Task<CustomerResponse> GetResponseByUser(Guid id)
        {
            var customer = await _customersRepository.GetByUser(id);
            var resposne = CustomerToResponse(customer);
            return resposne;
        }

        public async Task<CustomerResponse> GetShortCustomerData(Guid id)
        {
            var customer = await _customersRepository.GetById(id);
            return CustomerToResponse(customer);
        }

        private CustomerResponse CustomerToResponse(Customer customer)
        {
            var response = new CustomerResponse()
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.User.Email,
                PhoneNum = customer.User.PhoneNum
            };
            return response;
        }
    }
}
