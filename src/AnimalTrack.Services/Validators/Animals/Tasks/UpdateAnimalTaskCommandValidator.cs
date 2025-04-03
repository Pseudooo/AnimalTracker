using AnimalTrack.Services.Requests.Commands;
using FluentValidation;

namespace AnimalTrack.Services.Validators.Animals.Tasks;

public class UpdateAnimalTaskCommandValidator : AbstractValidator<UpdateAnimalTaskCommand>
{
    public UpdateAnimalTaskCommandValidator()
    {
        RuleFor(x => x)
            .SetValidator(new AnimalTaskBodyValidator());
    }
}