using AnimalTrack.ClientModels.Constants;
using AnimalTrack.ClientModels.Models.Animals;
using AnimalTrack.Repository.Interfaces;
using AnimalTrack.Services.Requests.Commands;
using MediatR;

namespace AnimalTrack.Services.Requests.Handlers;

public class CreateAnimalTaskHandler(IAnimalRepository animalRepository) 
    : IRequestHandler<CreateAnimalTaskCommand, AnimalTaskModel>
{
    public async Task<AnimalTaskModel> Handle(CreateAnimalTaskCommand request, CancellationToken cancellationToken)
    {
        var entity = await animalRepository.InsertAnimalTask(request.AnimalId, request.Name, request.Frequency, cancellationToken);
        if (!Enum.TryParse<SchedulingFrequency>(entity.Frequency, out var schedulingFrequency))
        {
            throw new Exception($"Invalid SchedulingFrequency value for task entity Id={entity.Id}: {schedulingFrequency}");
        }
        
        return new AnimalTaskModel()
        {
            Id = entity.Id,
            Name = entity.Name,
            CreatedAt = entity.CreatedAt,
            Frequency = schedulingFrequency,
        };
    }
}