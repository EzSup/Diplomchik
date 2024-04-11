using Restaurant.Client.Contracts.Cuisines;

namespace Restaurant.Client.Services.Interfaces
{
    public interface ICuisinesService
    {
        Task<ICollection<CuisineResponse>> GetAll();
        Task<string> Add(CuisineCreateRequest request);
    }
}
