using FluentValidation;
using FoodStore.BL.Helpers.DTOs.Product;

namespace FoodStore.BL.Helpers.Validators.Product;

public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
{
    public ProductCreateDtoValidator()
    {
        RuleFor(product => product.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Product name is required.")
            .MinimumLength(3)
            .WithMessage("Product name must be at least 3 characters long.");

        RuleFor(product => product.Price)
            .NotEmpty()
            .NotNull()
            .WithMessage("Product price is required.")
            .GreaterThan(0)
            .WithMessage("Price must be greater than 0.");

        RuleFor(product => product.Description)
            .MaximumLength(1000)
            .WithMessage("Description must be less than 1000 characters.");

        RuleFor(product => product.SKU)
            .NotEmpty()
            .NotNull()
            .WithMessage("SKU is required.")
            .Matches(@"^[A-Za-z0-9\-]+$")
            .WithMessage("SKU can only contain letters, numbers, and dashes.");

        RuleFor(product => product.StockQuantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Stock quantity cannot be negative.");

        RuleFor(product => product.CategoryId)
            .GreaterThan(0)
            .WithMessage("CategoryId is required and must be valid.");

        RuleForEach(product => product.TagIds)
            .GreaterThan(0)
            .WithMessage("TagIds must be greater than 0.");

        RuleFor(product => product.PrimaryImageUrl)
            .Must(IsValidUrl)
            .WithMessage("Primary image URL is invalid.");

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