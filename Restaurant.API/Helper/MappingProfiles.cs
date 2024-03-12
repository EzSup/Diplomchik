using AutoMapper;
using Restaurant.Core.Dtos;
using Restaurant.Core.Models;

namespace Restaurant.API.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Dish, DishDto>();
    }
}