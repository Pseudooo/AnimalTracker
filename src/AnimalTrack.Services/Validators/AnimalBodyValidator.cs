using AnimalTrack.ClientModels.Interfaces.Animal;
using FluentValidation;

namespace AnimalTrack.Services.Validators;

public class AnimalBodyValidator : AbstractValidator<IAnimalModel>
{
    public AnimalBodyValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();
    }
}