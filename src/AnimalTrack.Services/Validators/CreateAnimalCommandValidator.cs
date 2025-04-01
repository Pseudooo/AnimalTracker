using AnimalTrack.Services.Requests.Commands;
using FluentValidation;

namespace AnimalTrack.Services.Validators;

public class CreateAnimalCommandValidator : AbstractValidator<CreateAnimalCommand>
{
    public CreateAnimalCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();
    }
}