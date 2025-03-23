using AnimalTrack.ClientModels;
using AnimalTrack.Repository.Interfaces;
using AnimalTrack.Services.Requests.Queries;
using MediatR;

namespace AnimalTrack.Services.Requests.Handlers;

public class GetAnimalPageHandler(IAnimalRepository animalRepository)
    : IRequestHandler<GetAnimalPageQuery, List<AnimalModel>>
{
    public async Task<List<AnimalModel>> Handle(GetAnimalPageQuery request, CancellationToken cancellationToken)
    {
        var animalEntityPage = await animalRepository.GetAnimalPage(request.PageNumber, request.PageSize, cancellationToken);

        return animalEntityPage.Select(animalEntity => new AnimalModel()
            {
                Id = animalEntity.Id,
                Name = animalEntity.Name,
                CreatedAt = animalEntity.CreatedAt,
            })
            .ToList();
    }
}