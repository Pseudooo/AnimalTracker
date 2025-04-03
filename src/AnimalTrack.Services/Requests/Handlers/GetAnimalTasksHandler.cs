using AnimalTrack.ClientModels.Models.Animals;
using AnimalTrack.Repository.Interfaces;
using AnimalTrack.Services.Requests.Queries;
using MediatR;

namespace AnimalTrack.Services.Requests.Handlers;

public class GetAnimalTasksHandler(IAnimalRepository animalRepository)
    : IRequestHandler<GetAnimalTasksQuery, List<AnimalTaskModel>>
{
    public async Task<List<AnimalTaskModel>> Handle(GetAnimalTasksQuery request, CancellationToken cancellationToken)
    {
        var animalTaskModel = await animalRepository.GetAnimalTasks(request.AnimalId, cancellationToken);
        return animalTaskModel.Select(entity => new AnimalTaskModel
            {
                Id = entity.Id,
                Name = entity.Name,
                CreatedAt = entity.CreatedAt,
            })
            .ToList();
    }
}