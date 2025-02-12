using AutoMapper;
using FoodStore.BL.Helpers.DTOs.Tag;
using FoodStore.Core.Entities;

namespace FoodStore.BL.Helpers.Mapper;

public class TagMapper : Profile
{
    public TagMapper()
    {
        CreateMap<TagCreateDto, Tag>().ReverseMap();
        CreateMap<TagUpdateDto, Tag>()
            .ForMember(dest => dest.UpdatedTime, opt => opt.MapFrom(src => DateTime.UtcNow)).ReverseMap();
        CreateMap<TagGetDto, Tag>().ReverseMap();
    }
}