using FluentValidation;

namespace Restaurants.Application.Restaurants.Commands.ModifyRestaurant;

public class UpdateRestaurantCommandValidator : AbstractValidator<UpdateRestaurantCommand>
{
    public UpdateRestaurantCommandValidator()
    {
        RuleFor(dto => dto.Description)
           .NotEmpty().WithMessage("Description is required. ");

        RuleFor(dto => dto.Name)
          .Length(3, 100).WithMessage("Name length within 3-100 characters is required. ");
    }
}
