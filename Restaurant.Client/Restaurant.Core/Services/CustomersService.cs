using Microsoft.EntityFrameworkCore.Metadata;
using Restaurant.Core.DTOs;
using Restaurant.Core.DTOs.SmallDtos;
using Restaurant.Core.Models;
using Restaurant.Core.Repositories.Interfaces;
using Restaurant.Core.Services.Interfaces;
using Restaurant.Core.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Restaurant.Core.Functions.Interfaces;
using static Restaurant.Core.DTOs.Responses.CustomResponses;

namespace Restaurant.Core.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly ICustomersRepository _repository;
        private readonly IPasswordHasher _passwordHasher;

        public CustomersService(ICustomersRepository repository, 
            IPasswordHasher passwordHasher
            )
        {
            _repository = repository;
            _passwordHasher = passwordHasher;
        }

        public async Task<ICollection<Customer>> GetAll() => await _repository.GetAll();
        public async Task<Customer?> Get(int id) => await _repository.Get(id);
        public async Task<Customer?> Get(string email) => await _repository.Get(email);
        public async Task<int> Create(CustomerForCreateDto dto)
        {
            dto.Password = _passwordHasher.HashPassword(dto.Password);
            return await _repository.Create(dto);
        }
        public async Task<bool> Update(Customer dto) => await _repository.Update(dto);
        public async Task<bool> Delete(int id) => await _repository.Delete(id);

        public async Task<LogoChangingResponse> UpdateLogo(int customerId, string logoPath)
        {
            var customer = await Get(customerId);
            if(customer == null)
            {
                return new LogoChangingResponse(false, "User not found");
            }
            customer.PhotoLink = logoPath;
            if(await Update(customer))
            {
                return new LogoChangingResponse(true, "Logo updated successfully");
            }
            return new LogoChangingResponse(false, "Some error occured while updating user logo");
        }
        
    }
}
