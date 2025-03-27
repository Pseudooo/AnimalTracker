using AnimalTrack.Repository.Interfaces;
using AnimalTrack.Services.Requests.Commands;
using MediatR;

namespace AnimalTrack.Services.Requests.Handlers;

public class UpdateAnimalHandler(IAnimalRepository animalRepository)
    : IRequestHandler<UpdateAnimalCommand, bool>
{
    public async Task<bool> Handle(UpdateAnimalCommand request, CancellationToken cancellationToken)
    { 
         return await animalRepository.UpdateAnimal(request.Id, request.Name, cancellationToken);
    }
}