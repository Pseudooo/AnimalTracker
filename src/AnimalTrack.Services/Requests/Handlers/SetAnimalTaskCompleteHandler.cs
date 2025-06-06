using AnimalTrack.Repository.Interfaces;
using AnimalTrack.Services.Requests.Commands;
using MediatR;

namespace AnimalTrack.Services.Requests.Handlers;

public class SetAnimalTaskCompleteHandler(IAnimalRepository animalRepository) : IRequestHandler<SetAnimalTaskCompleteCommand, bool>
{
    public async Task<bool> Handle(SetAnimalTaskCompleteCommand request, CancellationToken cancellationToken)
    {
        return await animalRepository.CompleteAnimalTask(request.AnimalTaskId, cancellationToken);
    }
}