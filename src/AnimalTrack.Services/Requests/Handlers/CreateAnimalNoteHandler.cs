using AnimalTrack.ClientModels.Models.Animals;
using AnimalTrack.Repository.Interfaces;
using AnimalTrack.Services.Requests.Commands;
using MediatR;

namespace AnimalTrack.Services.Requests.Handlers;

public class CreateAnimalNoteHandler(IAnimalRepository animalRepository)
    : IRequestHandler<CreateAnimalNoteCommand, AnimalNoteModel>
{
    public async Task<AnimalNoteModel> Handle(CreateAnimalNoteCommand request, CancellationToken cancellationToken)
    {
        var noteEntity = await animalRepository.InsertAnimalNote(request.AnimalId, request.Note, cancellationToken);
        return new AnimalNoteModel()
        {
            Id = noteEntity.Id,
            Note = noteEntity.Note,
            CreatedAt = noteEntity.CreatedAt,
        };
    }
}