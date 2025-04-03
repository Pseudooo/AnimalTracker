using AnimalTrack.ClientModels.Interfaces.Animal;
using FluentValidation;

namespace AnimalTrack.Services.Validators.Animals.Tasks;

public class AnimalTaskBodyValidator : AbstractValidator<IAnimalTaskModel>
{
    public AnimalTaskBodyValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();
    }
}