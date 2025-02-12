using AutoMapper;
using FoodStore.BL.Helpers.DTOs.Auth;
using FoodStore.Core.Entities;

namespace FoodStore.BL.Helpers.Mapper;

public class AuthMapper : Profile
{
    public AuthMapper()
    {
        CreateMap<RegisterDto, AppUser>().ReverseMap();
    }
}