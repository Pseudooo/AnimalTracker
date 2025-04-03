using AnimalTrack.Services.Requests.Commands;
using FluentValidation;

namespace AnimalTrack.Services.Validators.Animals.Tasks;

public class CreateAnimalTaskCommandValidator : AbstractValidator<CreateAnimalTaskCommand>
{
    public CreateAnimalTaskCommandValidator()
    {
        RuleFor(x => x)
            .SetValidator(new AnimalTaskBodyValidator());
    }
}