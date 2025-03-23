using AnimalTrack.ClientModels;
using AnimalTrack.Repository.Interfaces;
using AnimalTrack.Services.Requests.Commands;
using MediatR;

namespace AnimalTrack.Services.Requests.Handlers;

public class CreateAnimalHandler(IAnimalRepository animalRepository)
    : IRequestHandler<CreateAnimalCommand, AnimalModel>
{
    public async Task<AnimalModel> Handle(CreateAnimalCommand request, CancellationToken cancellationToken)
    {
        var animalEntity = await animalRepository.InsertAnimal(request.Name, cancellationToken);
        return new AnimalModel()
        {
            Id = animalEntity.Id,
            CreatedAt = animalEntity.CreatedAt,
            Name = animalEntity.Name,
        };
    }
}