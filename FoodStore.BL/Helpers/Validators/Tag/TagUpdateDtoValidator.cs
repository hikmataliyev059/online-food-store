using FluentValidation;
using FoodStore.BL.Helpers.DTOs.Tag;

namespace FoodStore.BL.Helpers.Validators.Tag;

public class TagUpdateDtoValidator : AbstractValidator<TagUpdateDto>
{
    public TagUpdateDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Tag name cannot be empty")
            .MinimumLength(3)
            .WithMessage("Tag name must be at least 3 characters long")
            .MaximumLength(50)
            .WithMessage("Tag name must be between 3 and 50 characters");
    }
}