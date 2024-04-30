using Restaurant.Application.Interfaces.Services;
using Restaurant.Core.Dtos.Reviews;
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

        public async Task<ICollection<Review>> GetByFilter(int pageIndex, int pageSize, Guid? DishId = null, Guid? AuthorId = null, double minRate = 1, double maxRate = 5)
            => await _reviewsRepository.GetByFilter(pageIndex, pageSize, DishId, AuthorId, minRate, maxRate);    

        public async Task<Review> GetById(Guid id) => await _reviewsRepository.GetById(id);

        public async Task<ICollection<Review>> GetByPage(int page, int pageSize) 
            => await _reviewsRepository.GetByPage(page, pageSize);
         
        public async Task<int> Purge(IEnumerable<Guid> values)
            => await _reviewsRepository.Purge(values);

        public async Task<bool> Update(Review entity)
            => await _reviewsRepository.Update(entity);

        public async Task<ICollection<ReviewOfDishResponse>> GetReviewsOfDish(Guid id, int pageIndex, int pageSize)
        {
            var reviews =  await GetByFilter(pageIndex, pageSize, id);
            List<ReviewOfDishResponse> result = new();
            foreach(var review in reviews)
            {
                result.Add(new ReviewOfDishResponse()
                {
                    Title = review.Title,
                    Content = review.Content,
                    Rate = review.Rate,
                    Posted = review.Posted,
                    AuthorName = review?.Author?.Name ?? string.Empty
                });
            }
            return result;
        }
    }
}
