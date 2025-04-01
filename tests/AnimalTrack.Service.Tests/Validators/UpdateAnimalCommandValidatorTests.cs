using AnimalTrack.Services.Requests.Commands;
using AnimalTrack.Services.Validators;
using FluentValidation.TestHelper;

namespace AnimalTrack.Service.Tests.Validators;

public class UpdateAnimalCommandValidatorTests
{
    private readonly UpdateAnimalCommandValidator _validator = new();
    
    [Fact]
    public void GivenValidCommand_WhenValidate_ShouldPass()
    {
        // Arrange
        var command = new UpdateAnimalCommand(8008, "John");
        
        // Act
        var result = _validator.TestValidate(command);
        
        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    public void GivenInvalidCommand_WhenValidate_ShouldFail(string name)
    {
        // Arrange
        var command = new UpdateAnimalCommand(420, name);
        
        // Act
        var result = _validator.TestValidate(command);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
}