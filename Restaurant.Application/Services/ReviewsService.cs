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
    public class ReviewsService : IReviewsService
    {
        private readonly IReviewsRepository _reviewsRepository;

        public ReviewsService(IReviewsRepository reviewsRepository)
        {
            _reviewsRepository = reviewsRepository;
        }

        public async Task<Guid> Add(Review entity) => await _reviewsRepository.Add(entity);

        public async Task<bool> Delete(Guid id) => await _reviewsRepository.Delete(id);

        public async Task<ICollection<Review>> GetAll() => await _reviewsRepository.GetAll();

        public async Task<ICollection<Review>> GetByFilter(Guid? DishId = null, Guid? AuthorId = null, double minRate = 1, double maxRate = 5)
            => await _reviewsRepository.GetByFilter(DishId, AuthorId, minRate, maxRate);    

        public async Task<Review> GetById(Guid id) => await _reviewsRepository.GetById(id);

        public async Task<ICollection<Review>> GetByPage(int page, int pageSize) 
            => await _reviewsRepository.GetByPage(page, pageSize);
         
        public async Task<int> Purge(IEnumerable<Guid> values)
            => await _reviewsRepository.Purge(values);

        public async Task<bool> Update(Review entity)
            => await _reviewsRepository.Update(entity);
    }
}
