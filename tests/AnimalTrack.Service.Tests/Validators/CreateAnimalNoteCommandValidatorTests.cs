using AnimalTrack.Services.Requests.Commands;
using AnimalTrack.Services.Validators;
using FluentValidation.TestHelper;

namespace AnimalTrack.Service.Tests.Validators;

public class CreateAnimalNoteCommandValidatorTests
{
    private readonly CreateAnimalNoteCommandValidator _validator = new();
    
    [Fact]
    public void GivenValidCommand_WhenValidate_ShouldPass()
    {
        // Arrange
        var command = new CreateAnimalNoteCommand(69, "John");
        
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
        var command = new CreateAnimalNoteCommand(420, name);
        
        // Act
        var result = _validator.TestValidate(command);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Note);
    }
}