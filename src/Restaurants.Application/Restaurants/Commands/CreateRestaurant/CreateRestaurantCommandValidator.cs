

using FluentValidation;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
{
    private readonly List<string> validCategories = ["Italian", "Mexican", "Japanese", "Indian"];
    public CreateRestaurantCommandValidator()
    {
        RuleFor(dto => dto.Category)
            .Custom((val, ctx) =>
            {
                var isValidCategory = validCategories.Contains(val);
                if (!isValidCategory)
                {
                    ctx.AddFailure("Category", "Invalid category. Please provide a valid category");
                }
            });

        RuleFor(dto => dto.Name)
            .Length(3, 100);

        RuleFor(dto => dto.Description)
            .NotEmpty().WithMessage("Description is required. ");

        RuleFor(dto => dto.ContactEmail)
            .EmailAddress().WithMessage("Please provide valid email address");

        //RuleFor(dto => dto.Category)
        //    .NotEmpty().WithMessage("Please provide a valid category");

        RuleFor(dto => dto.PostalCode)
            .Matches(@"^\d{2}-\d{3}$")
            .WithMessage("Please provid a valid postal code (XX-XXX)");
    }
}