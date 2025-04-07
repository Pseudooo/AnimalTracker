using AnimalTrack.Repository.Interfaces;
using AnimalTrack.Services.Requests.Commands;
using MediatR;

namespace AnimalTrack.Services.Requests.Handlers;

public class UpdateAnimalTaskHandler(IAnimalRepository animalRepository) 
    : IRequestHandler<UpdateAnimalTaskCommand, bool>
{
    public async Task<bool> Handle(UpdateAnimalTaskCommand request, CancellationToken cancellationToken)
    {
        return await animalRepository.UpdateAnimalTask(
            request.AnimalTaskId,
            request.Name,
            request.Frequency,
            request.ScheduledFor,
            cancellationToken);;
    }
}