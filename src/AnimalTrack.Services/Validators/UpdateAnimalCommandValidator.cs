using AnimalTrack.Services.Requests.Commands;
using FluentValidation;

namespace AnimalTrack.Services.Validators;

public class UpdateAnimalCommandValidator : AbstractValidator<UpdateAnimalCommand>
{
    public UpdateAnimalCommandValidator()
    {
        RuleFor(x => x)
            .SetValidator(new AnimalBodyValidator());
    }
}