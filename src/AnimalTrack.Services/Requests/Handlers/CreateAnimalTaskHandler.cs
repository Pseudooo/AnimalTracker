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
        var entity = await animalRepository.InsertAnimalTask(request.AnimalId, request.Name, cancellationToken);
        return new AnimalTaskModel()
        {
            Id = entity.Id,
            Name = entity.Name,
            CreatedAt = entity.CreatedAt,
        };
    }
}