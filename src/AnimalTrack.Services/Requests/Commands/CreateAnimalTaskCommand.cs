using AnimalTrack.ClientModels.Interfaces.Animal;
using AnimalTrack.ClientModels.Models.Animals;

namespace AnimalTrack.Services.Requests.Commands;

public record CreateAnimalTaskCommand(int AnimalId, string Name) : ICommand<AnimalTaskModel>, IAnimalTaskModel;