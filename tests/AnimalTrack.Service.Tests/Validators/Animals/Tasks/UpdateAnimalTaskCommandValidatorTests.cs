using AnimalTrack.ClientModels.Constants;
using AnimalTrack.Services.Requests.Commands;
using AnimalTrack.Services.Validators.Animals.Tasks;
using FluentValidation.TestHelper;

namespace AnimalTrack.Service.Tests.Validators.Animals.Tasks;

public class UpdateAnimalTaskCommandValidatorTests
{
    private readonly UpdateAnimalTaskCommandValidator _validator = new();
    
    [Fact]
    public void GivenValidCommand_WhenValidate_ShouldPass()
    {
        // Arrange
        var command = new UpdateAnimalTaskCommand(10, "John", SchedulingFrequency.OneOff, new DateOnly(2025, 08, 27));
        
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
        var command = new UpdateAnimalTaskCommand(10, name, SchedulingFrequency.OneOff, new DateOnly(2025, 08, 27));
        
        // Act
        var result = _validator.TestValidate(command);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
}