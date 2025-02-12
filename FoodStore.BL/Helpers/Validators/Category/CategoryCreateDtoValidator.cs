using FluentValidation;
using FoodStore.BL.Helpers.DTOs.Category;

namespace FoodStore.BL.Helpers.Validators.Category;

public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
{
    public CategoryCreateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Name cannot be empty")
            .MinimumLength(3)
            .WithMessage("Category name must be at least 3 characters long")
            .MaximumLength(50)
            .WithMessage("Category name must be between 3 and 50 characters");
    }
}