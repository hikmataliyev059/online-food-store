using AutoMapper;
using FoodStore.BL.Helpers.DTOs.Product;
using FoodStore.Core.Entities;

namespace FoodStore.BL.Helpers.Mapper;

public class ProductMapper : Profile
{
    public ProductMapper()
    {
        CreateMap<Product, ProductGetDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.TagIds, opt => opt.MapFrom(src => src.TagProducts.Select(tp => tp.TagId).ToList()))
            .ForMember(dest => dest.TagNames,
                opt => opt.MapFrom(src => src.TagProducts.Select(tp => tp.Tag.Name).ToList()))
            .ForMember(dest => dest.PrimaryImageUrl, opt =>
                opt.MapFrom(src =>
                    src.ProductImages.FirstOrDefault(p => p.Primary) != null
                        ? src.ProductImages.FirstOrDefault(p => p.Primary)!.ImgUrl
                        : null))
            .ForMember(dest => dest.SubImageUrls, opt =>
                opt.MapFrom(src => src.ProductImages.Where(p => !p.Primary).Select(p => p.ImgUrl).ToList()));


        CreateMap<ProductCreateDto, Product>()
            .ForMember(dest => dest.TagProducts,
                opt => opt.MapFrom(src => src.TagIds.Select(id => new TagProduct { TagId = id }).ToList()))
            .ForMember(dest => dest.ProductImages,
                opt => opt.MapFrom(src =>
                    src.SubImageUrls.Select(url => new ProductImage { ImgUrl = url, Primary = false }).ToList()))
            .ForMember(dest => dest.ProductImages,
                opt => opt.MapFrom((src, dest) =>
                    new List<ProductImage> { new ProductImage { ImgUrl = src.PrimaryImageUrl, Primary = true } }
                        .Concat(src.SubImageUrls.Select(url => new ProductImage { ImgUrl = url, Primary = false }))
                        .ToList()));


        CreateMap<Product, MainShopProductDto>()
            .ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Price,
                opt => opt.MapFrom(src => src.Price))
            .ForMember(dest => dest.ShortDescription,
                opt => opt.MapFrom(src =>
                    src.Description.Length > 100 ? src.Description.Substring(0, 100) + "..." : src.Description))
            .ForMember(dest => dest.ImageUrls,
                opt => opt.MapFrom(src => src.ProductImages.Select(pi => pi.ImgUrl).ToList()));

        CreateMap<ProductUpdateDto, Product>()
            .ForMember(dest => dest.TagProducts,
                opt => opt.MapFrom(src => src.TagIds.Select(id => new TagProduct { TagId = id }).ToList()))
            .ForMember(dest => dest.ProductImages,
                opt => opt.MapFrom((src, dest) =>
                {
                    var productImages = new List<ProductImage>();

                    // İlk resme Primary = true olarak atama
                    if (!string.IsNullOrEmpty(src.PrimaryImageUrl))
                    {
                        productImages.Add(new ProductImage
                        {
                            ImgUrl = src.PrimaryImageUrl,
                            Primary = true
                        });
                    }

                    // SubImageUrls'ları Primary = false olarak ekliyoruz
                    if (src.SubImageUrls != null && src.SubImageUrls.Any())
                    {
                        productImages.AddRange(src.SubImageUrls.Select(url => new ProductImage
                        {
                            ImgUrl = url,
                            Primary = false
                        }));
                    }

                    return productImages;
                }))
            .ForMember(dest => dest.UpdatedTime, opt => opt.MapFrom(src => DateTime.UtcNow));  // Burada manuel olarak güncelleniyor

// ReverseMap için
        CreateMap<Product, ProductUpdateDto>()
            .ForMember(dest => dest.TagIds, opt => opt.MapFrom(src => src.TagProducts.Select(tp => tp.TagId).ToList()))
            .ForMember(dest => dest.PrimaryImageUrl,
                opt => opt.MapFrom(src => src.ProductImages.FirstOrDefault(p => p.Primary) != null ? src.ProductImages.FirstOrDefault(p => p.Primary).ImgUrl : null))  // İlk resmi Primary olarak alıyoruz
            .ForMember(dest => dest.SubImageUrls,
                opt => opt.MapFrom(src => src.ProductImages.Where(p => !p.Primary).Select(pi => pi.ImgUrl).ToList()))  // Diğerlerini SubImage olarak alıyoruz
            .ReverseMap();  // İki yönlü dönüşüm sağlıyoruz

    }
}