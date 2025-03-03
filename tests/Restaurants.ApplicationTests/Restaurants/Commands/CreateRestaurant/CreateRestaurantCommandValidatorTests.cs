using FluentValidation.TestHelper;
using Xunit;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests;

public class CreateRestaurantCommandValidatorTests
{
    [Fact()]
    public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
    {
        // arrange

        var command = new CreateRestaurantCommand()
        {
            Name = "Test",
            Category = "Mexican",
            ContactEmail = "Test@test.com",
            PostalCode = "12-345",
            Description = "Description"
        };

        // act
        var validator = new CreateRestaurantCommandValidator();
        var result = validator.TestValidate(command);

        // assert

        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact()]
    public void Validator_ForInValidCommand_ShouldHaveValidationErrors()
    {
        // arrange

        var command = new CreateRestaurantCommand()
        {
            Name = "Te",
            Category = "Mexica",
            ContactEmail = "@test.com",
            PostalCode = "12345",
            Description = "Description"
        };

        // act
        var validator = new CreateRestaurantCommandValidator();
        var result = validator.TestValidate(command);

        // assert

        result.ShouldHaveValidationErrorFor(c => c.Name);
        result.ShouldHaveValidationErrorFor(c => c.Category);
        result.ShouldHaveValidationErrorFor(c => c.ContactEmail);
        result.ShouldHaveValidationErrorFor(c => c.PostalCode);

    }

    [Theory()]
    [InlineData("Italian")]
    [InlineData("Mexican")]
    [InlineData("Japanese")]
    [InlineData("Indian")]
    public void Validator_ForValidCategory_ShouldNotHaveValidationErrorsForCategoryProperty(string category)
    {
        // arrange
        var validator = new CreateRestaurantCommandValidator();
        var command = new CreateRestaurantCommand { Category = category };

        // act
        var result = validator.TestValidate(command);

        // assert

        result.ShouldNotHaveValidationErrorFor(c => c.Category);
    }

    [Theory()]
    [InlineData("10220")]
    [InlineData("1022-230")]
    [InlineData("10 220")]
    [InlineData("10-2 20")]
    public void Validator_ForInvalidPostalCode_ShouldHaveValidationErrorsForPostalCodeProperty(string postalCode)
    {
        // arrange
        var validator = new CreateRestaurantCommandValidator();
        var command = new CreateRestaurantCommand { PostalCode = postalCode };

        // act
        var result = validator.TestValidate(command);

        // assert

        result.ShouldHaveValidationErrorFor(c => c.PostalCode);
    }

} 