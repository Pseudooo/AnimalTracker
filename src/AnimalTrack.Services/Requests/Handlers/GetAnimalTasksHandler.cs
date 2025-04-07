using AnimalTrack.ClientModels.Constants;
using AnimalTrack.ClientModels.Models.Animals;
using AnimalTrack.Repository.Entities;
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
        return animalTaskModel.Select(Map)
            .ToList();
    }

    private static AnimalTaskModel Map(AnimalTaskEntity entity)
    {
        if (!Enum.TryParse<SchedulingFrequency>(entity.Frequency, out var frequency))
        {
            throw new Exception($"Invalid SchedulingFrequency value for task entity Id={entity.Id}: {entity.Frequency}");
        }

        return new AnimalTaskModel()
        {
            Id = entity.Id,
            Name = entity.Name,
            CreatedAt = entity.CreatedAt,
            Frequency = frequency
        };
    }
}