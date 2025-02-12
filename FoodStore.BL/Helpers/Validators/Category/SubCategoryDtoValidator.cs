// using FluentValidation;
// using FoodStore.BL.Helpers.Constants;
// using FoodStore.BL.Helpers.DTOs.Category;
//
// namespace FoodStore.BL.Helpers.Validators.Category;
//
// public class SubCategoryDtoValidator : AbstractValidator<SubCategoryDto>
// {
//     public SubCategoryDtoValidator()
//     {
//         RuleFor(x => x.SubId)
//             .NotEmpty()
//             .NotNull()
//             .WithMessage(ValidationMessages.Required);
//
//         RuleFor(x => x.SubName)
//             .NotEmpty()
//             .NotNull()
//             .WithMessage(ValidationMessages.Required)
//             .MaximumLength(100)
//             .WithMessage(ValidationMessages.MaxLength);
//     }
// }