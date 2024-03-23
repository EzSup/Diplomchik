using Microsoft.EntityFrameworkCore.Metadata;
using Restaurant.Core.Dtos;
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

namespace Restaurant.Core.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly ICustomersRepository _repository;

        public CustomersService(ICustomersRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICollection<Customer>> GetAll() => await _repository.GetAll();
        public async Task<Customer?> Get(int id) => await _repository.Get(id);
        public async Task<int> Create(CustomerForCreateDto dto) => await _repository.Create(dto);
        public async Task<bool> Update(CustomerDto dto) => await _repository.Update(dto);
        public async Task<bool> Delete(int id) => await _repository.Delete(id);
        
    }
}
