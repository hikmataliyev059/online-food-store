using AutoMapper;
using FoodStore.BL.Helpers.DTOs.Category;
using FoodStore.Core.Entities;

namespace FoodStore.BL.Helpers.Mapper;

public class CategoryMapper : Profile
{
    public CategoryMapper()
    {
        CreateMap<CategoryCreateDto, Category>().ReverseMap();
        CreateMap<CategoryUpdateDto, Category>()
            .ForMember(dest => dest.UpdatedTime, opt => opt.MapFrom(src => DateTime.UtcNow)).ReverseMap();
        CreateMap<CategoryGetDto, Category>().ReverseMap();

        CreateMap<SubCategoryCreateDto, SubCategory>().ReverseMap();
        CreateMap<SubCategoryUpdateDto, SubCategory>()
            .ForMember(dest => dest.UpdatedTime, opt => opt.MapFrom(src => DateTime.UtcNow)).ReverseMap();

        CreateMap<SubCategory, SubCategoryGetDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ReverseMap();

        CreateMap<SubCategory, SubCategoryDto>()
            .ForMember(dest => dest.SubCategoryId, opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.SubCategoryName, opt => opt.MapFrom(src => src.Name));

        CreateMap<Category, CategoryWithSubcategoriesDto>()
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Subcategories, opt => opt.MapFrom(src => src.SubCategories));
    }
}