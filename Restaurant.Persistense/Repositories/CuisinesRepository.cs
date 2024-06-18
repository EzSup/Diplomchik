﻿using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Interfaces;
using Restaurant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Persistense.Repositories
{
    public class CuisinesRepository : ICuisinesRepository
    {
        private readonly RestaurantDbContext _context;

        public CuisinesRepository(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Add(Cuisine entity)
        {
            var model = new Cuisine()
            {
                Name = entity.Name,
                DiscountId = entity.DiscountId
            };
            await _context.Cuisines.AddAsync(model);
            await _context.SaveChangesAsync();
            return model.Id;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _context.Cuisines
                .Where(x => x.Id == id)
                .ExecuteDeleteAsync() == 1;
        }

        public async Task<ICollection<Cuisine>> GetAll()
        {
            return await _context.Cuisines
                .AsNoTracking()
                .Include(x => x.Discount)
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<Cuisine> GetById(Guid id)
        {
            return await _context.Cuisines.AsNoTracking()
                .Include(x => x.Discount)
                .FirstOrDefaultAsync(x => x.Id == id) ?? throw new KeyNotFoundException("Кухня не знайдена!");
        }

        public async Task<ICollection<Cuisine>> GetByPage(int page, int pageSize)
        {
            if (pageSize > 0 && page > 0)
            {
                return await _context.Cuisines.AsNoTracking()
                    .Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            throw new ArgumentException("Кількість і номер сторінок повинні бути більші за 0!");
        }

        public async Task<int> Purge(IEnumerable<Guid> values)
        {
            return await _context.Cuisines
                .Where(t => values.Contains(t.Id))
                .ExecuteDeleteAsync();
        }

        public async Task<bool> Update(Cuisine entity)
        {
            return await _context.Cuisines
                .Where(r => r.Id == entity.Id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(r => r.Name, entity.Name)
                    .SetProperty(r => r.DiscountId, entity.DiscountId == Guid.Empty ? null : entity.DiscountId)) == 1;
        }
    }
}
