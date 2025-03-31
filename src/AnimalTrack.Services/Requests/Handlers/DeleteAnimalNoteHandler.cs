using AnimalTrack.Repository.Interfaces;
using AnimalTrack.Services.Requests.Commands;
using MediatR;

namespace AnimalTrack.Services.Requests.Handlers;

public class DeleteAnimalNoteHandler(IAnimalRepository animalRepository)
    : IRequestHandler<DeleteAnimalNoteCommand, bool>
{
    public async Task<bool> Handle(DeleteAnimalNoteCommand request, CancellationToken cancellationToken)
    {
        return await animalRepository.DeleteAnimalNote(request.NoteId, cancellationToken);
    }
}