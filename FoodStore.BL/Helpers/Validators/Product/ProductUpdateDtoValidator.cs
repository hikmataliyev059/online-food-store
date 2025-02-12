using FluentValidation;
using FoodStore.BL.Helpers.DTOs.Product;

namespace FoodStore.BL.Helpers.Validators.Product;

public class ProductUpdateDtoValidator : AbstractValidator<ProductUpdateDto>
{
    public ProductUpdateDtoValidator()
    {
        RuleFor(product => product.Name)
            .NotEmpty()
            .When(product => !string.IsNullOrEmpty(product.Name))
            .WithMessage("Product name cannot be empty if provided.")
            .MinimumLength(3)
            .When(product => !string.IsNullOrEmpty(product.Name))
            .WithMessage("Product name must be at least 3 characters long.");

        RuleFor(product => product.Price)
            .GreaterThan(0)
            .When(product => product.Price.HasValue)
            .WithMessage("Price must be greater than 0 if provided.");

        RuleFor(product => product.Description)
            .MaximumLength(1000)
            .When(product => !string.IsNullOrEmpty(product.Description))
            .WithMessage("Description must be less than 1000 characters.");

        RuleFor(product => product.SKU)
            .Matches(@"^[A-Za-z0-9\-]+$")
            .When(product => !string.IsNullOrEmpty(product.SKU))
            .WithMessage("SKU can only contain letters, numbers, and dashes.");

        RuleFor(product => product.StockQuantity)
            .GreaterThanOrEqualTo(0)
            .When(product => product.StockQuantity.HasValue)
            .WithMessage("Stock quantity cannot be negative.");

        RuleFor(product => product.CategoryId)
            .GreaterThan(0)
            .When(product => product.CategoryId.HasValue)
            .WithMessage("CategoryId must be valid if provided.");

        RuleForEach(product => product.TagIds)
            .GreaterThan(0)
            .When(product => product.TagIds.Any())
            .WithMessage("TagIds must be greater than 0.");

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