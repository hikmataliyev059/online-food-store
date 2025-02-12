using FluentValidation;
using FoodStore.BL.Helpers.DTOs.Product;

namespace FoodStore.BL.Helpers.Validators.Product;

public class ProductGetDtoValidator : AbstractValidator<ProductGetDto>
{
    public ProductGetDtoValidator()
    {
        RuleFor(product => product.Id)
            .GreaterThan(0)
            .WithMessage("Product ID must be a valid number.");

        RuleFor(product => product.Name)
            .NotEmpty()
            .WithMessage("Product name is required.");

        RuleFor(product => product.Price)
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0.");

        RuleFor(product => product.Description)
            .MaximumLength(1000)
            .WithMessage("Description must be less than 1000 characters.");

        RuleFor(product => product.SKU)
            .NotEmpty()
            .WithMessage("SKU is required.");

        RuleFor(product => product.StockQuantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Stock quantity cannot be negative.");

        RuleFor(product => product.CategoryName)
            .NotEmpty()
            .WithMessage("Category name is required.");

        RuleForEach(product => product.TagIds)
            .GreaterThan(0)
            .WithMessage("TagIds  must be greater than 0.");

        RuleForEach(product => product.SubImageUrls)
            .Must(IsValidUrl)
            .WithMessage("Each image URL must be a valid URL.");
    }

    private static bool IsValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var result)
               && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
    }
}