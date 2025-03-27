using AnimalTrack.ClientModels.Models.Animals;
using AnimalTrack.Repository.Interfaces;
using AnimalTrack.Services.Requests.Queries;
using MediatR;

namespace AnimalTrack.Services.Requests.Handlers;

public class GetAnimalNotesHandler(IAnimalRepository animalRepository)
    : IRequestHandler<GetAnimalNotesQuery, List<AnimalNoteModel>>
{
    public async Task<List<AnimalNoteModel>> Handle(GetAnimalNotesQuery request, CancellationToken cancellationToken)
    {
        var noteEntities = await animalRepository.GetAnimalNotes(request.AnimalId, cancellationToken);
        return noteEntities.Select(note => new AnimalNoteModel
            {
                Id = note.Id,
                Note = note.Note,
                CreatedAt = note.CreatedAt
            })
            .ToList();
    }
}