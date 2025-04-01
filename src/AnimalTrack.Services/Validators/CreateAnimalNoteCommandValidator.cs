using AnimalTrack.Services.Requests.Commands;
using FluentValidation;

namespace AnimalTrack.Services.Validators;

public class CreateAnimalNoteCommandValidator : AbstractValidator<CreateAnimalNoteCommand>
{
    public CreateAnimalNoteCommandValidator()
    {
        RuleFor(x => x.Note)
            .NotEmpty();
    }
}