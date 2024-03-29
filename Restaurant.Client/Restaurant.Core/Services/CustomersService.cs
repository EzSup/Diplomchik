﻿using Microsoft.EntityFrameworkCore.Metadata;
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
        public async Task<Customer?> Get(string email) => await _repository.Get(email);
        public async Task<int> Create(CustomerForCreateDto dto)=> await _repository.Create(dto);        
        public async Task<bool> Update(Customer dto) => await _repository.Update(dto);
        public async Task<bool> Delete(int id) => await _repository.Delete(id);

        public async Task<Customer?> LogIn(ICustomersService.LogInData customer)
        {
            var realCustomer = await _repository.Get(customer.email!);
            if(realCustomer == null) {
                throw new InvalidOperationException("Такого користувача немає!");
            }
            if (Security.PasswordsMatch(customer.password, realCustomer.Password))
            {
                return realCustomer;
            }
            return null;
        }

        public async Task<bool> UpdateLogo(int customerId, string logoPath)
        {
            var customer = await Get(customerId);
            if(customer == null)
            {
                return false;
            }
            customer.PhotoLink = logoPath;
            if(await Update(customer))
            {
                return true;
            }
            return false;
        }
        
    }
}
