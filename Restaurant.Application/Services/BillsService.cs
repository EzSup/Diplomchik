﻿using Restaurant.Application.Interfaces.Services;
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
    public class BillsService : IBillsService
    {
        private readonly IBillsRepository _billsRepository;
        private readonly ICartsService _cartsService;
        private readonly ICustomersService _customersService;

        public BillsService(IBillsRepository repository, ICartsService cartsService, ICustomersService customersService)
        {
            _billsRepository = repository; 
            _cartsService = cartsService;
            _customersService = customersService;
        }
        public async Task<Bill> GetById(Guid Id) => await _billsRepository.GetById(Id);
        public async Task<ICollection<Bill>> GetAll() => await _billsRepository.GetAll();
        public async Task<BillResponse> GetResponseById(Guid Id)
        {
            var bill = await _billsRepository.GetById(Id);
            var response = new BillResponse();
            response.Id = Id;
            response.TotalPrice = bill.TotalPrice;
            response.OrderDateAndTime = bill.OrderDateAndTime;
            response.Tips = bill.Tips;
            response.Type = bill.Type;
            if (response.Type == Bill.OrderType.InRestaurant)
                response.ReservationOrDeliveryId = bill.ReservationId;
            else
                response.ReservationOrDeliveryId = bill.DeliveryId;
            response.IsPaid = bill.IsPaid;

            response.Dishes = bill.Cart.DishCarts
                .Select(x => new DishOfBill(x.Dish.Name, x.Count, x.Dish.Price, x.Count * x.Dish.Price)).ToList();
            return response;
        }
        public async Task<ICollection<Bill>> GetByPage(int page, int pageSize) => await _billsRepository.GetByPage(page, pageSize);
        public async Task<ICollection<Bill>> GetByFilter(int pageIndex, int pageSize, decimal MinPrice = 0, decimal MaxPrice = decimal.MaxValue,
            DateTime? minOrderDateTime = null, DateTime? maxOrderDateTime = null,
            Bill.OrderType? orderType = null, int minTipsPercents = 0, int maxTipsPercents = 100,
            Guid? customerId = null, Guid? reservationId = null, Guid? deliveryId = null)
            => await _billsRepository.GetByFilter(pageIndex, pageSize, MinPrice, MaxPrice,
            minOrderDateTime, maxOrderDateTime,
            orderType, minTipsPercents, maxTipsPercents,
            customerId,reservationId ,deliveryId );

        public async Task<Guid> Add(Bill obj) 
        {
            var customer = await _customersService.GetById(obj.CustomerId);
            obj.CartId = (Guid)customer.CartId;
            customer.CartId = await _cartsService.Add(new());
            await _customersService.Update(customer);
            return await _billsRepository.Add(obj);         
        }

        public async Task<ICollection<BillResponse>> GetBillsOfCustomer(int pageIndex, int pageSize,Guid CustomerId)
        {
            var bills = await _billsRepository.GetByFilter(pageIndex, pageSize, customerId: CustomerId);
            ICollection<BillResponse> responses = new List<BillResponse>();
            foreach (var bill in bills)
            {
                responses.Add(await GetResponseById(bill.Id));
            }
            return responses;
        }

        public async Task<BillResponse> RegisterBill(Bill obj)
        {            
            var billId = await Add(obj);
            return await GetResponseById(billId);
        }
        public async Task<bool> Update(Bill obj) => await _billsRepository.Update(obj);
        public async Task<bool> Delete(Guid id) => await _billsRepository.Delete(id);
        public async Task<int> Purge(IEnumerable<Guid> ids) => await _billsRepository.Purge(ids);

        public async Task<(bool flag, string message, decimal rest)> Pay(Guid BillId, decimal Amount, int TipsPercents)
        {
            var bill = await GetById(BillId);

            if (bill.IsPaid)
            {
                return (false, "Bill is already paid", 0);
            }

            var sum = bill.TotalPrice;

            var sumWithTips = sum + sum * 0.01m * TipsPercents;
            if(Amount >= sumWithTips)
            {                
                bill.PaidAmount = sum;
                bill.Tips = TipsPercents*sum;
                bill.IsPaid = true;
                await Update(bill);
                return (true, "Successfully paid, full tips", Amount - sumWithTips);
            }
            else if(Amount < sumWithTips && Amount >= sum) {
                bill.PaidAmount = sum;
                bill.Tips = Amount - sum;
                bill.IsPaid = true;
                await Update(bill);
                return (true, "Successfully paid, not full tips", 0);
            }
            else
            {
                return (false, "Unsuccess, not enough amount", Amount);
            }
        }
    }
}
