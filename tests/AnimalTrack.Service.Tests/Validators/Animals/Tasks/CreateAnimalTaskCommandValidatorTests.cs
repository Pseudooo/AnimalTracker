using AnimalTrack.Services.Requests.Commands;
using AnimalTrack.Services.Validators.Animals.Tasks;
using FluentValidation.TestHelper;

namespace AnimalTrack.Service.Tests.Validators.Animals.Tasks;

public class CreateAnimalTaskCommandValidatorTests
{
    private readonly CreateAnimalTaskCommandValidator _validatorTests = new();
    
    [Fact]
    public void GivenValidCommand_WhenValidate_ShouldPass()
    {
        // Arrange
        var command = new CreateAnimalTaskCommand(10, "John");
        
        // Act
        var result = _validatorTests.TestValidate(command);
        
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
        var command = new CreateAnimalTaskCommand(10, name);
        
        // Act
        var result = _validatorTests.TestValidate(command);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
}