using AnimalTrack.ClientModels;
using AnimalTrack.Repository.Interfaces;
using AnimalTrack.Services.Requests.Queries;
using MediatR;

namespace AnimalTrack.Services.Requests.Handlers;

public class GetAnimalByIdHandler(IAnimalRepository animalRepository)
    : IRequestHandler<GetAnimalByIdQuery, AnimalModel?>
{
    public async Task<AnimalModel?> Handle(GetAnimalByIdQuery request, CancellationToken cancellationToken)
    {
        var animalEntity = await animalRepository.GetAnimalById(request.Id, cancellationToken);
        if (animalEntity is null)
            return null;
        
        return new AnimalModel()
        {
            Id = animalEntity.Id,
            Name = animalEntity.Name,
            CreatedAt = animalEntity.CreatedAt,
        };
    }
}