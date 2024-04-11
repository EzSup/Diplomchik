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
    public class CuisinesService : ICuisinesService
    {
        private readonly ICuisinesRepository _cuisinesRepository;

        public CuisinesService(ICuisinesRepository cuisinesRepository)
        {
            _cuisinesRepository = cuisinesRepository;
        }

        public async Task<Guid> Add(Cuisine entity)
            => await _cuisinesRepository.Add(entity);

        public async Task<bool> Delete(Guid id)
            => await (_cuisinesRepository.Delete(id));

        public async Task<ICollection<Cuisine>> GetAll()
            => await _cuisinesRepository.GetAll();

        public async Task<Cuisine> GetById(Guid id)
            => await _cuisinesRepository.GetById(id);

        public async Task<ICollection<Cuisine>> GetByPage(int page, int pageSize)
            => await _cuisinesRepository.GetByPage(page, pageSize);

        public async Task<int> Purge(IEnumerable<Guid> values)
            => await _cuisinesRepository.Purge(values);

        public async Task<bool> Update(Cuisine entity)
            => await _cuisinesRepository.Update(entity);
    }
}
