using Restaurant.Core.Dtos;
using Restaurant.Core.Models;

namespace Restaurant.Core.Repositories.Interfaces;

public interface ICuisinesRepository : ICRUDRepo<Cuisine, CuisineForCreateDto>
{
}