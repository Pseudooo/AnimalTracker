using AnimalTrack.ClientModels.Interfaces.Animal;

namespace AnimalTrack.Services.Requests.Commands;

public record UpdateAnimalTaskCommand(int AnimalTaskId, string Name) : ICommand<bool>, IAnimalTaskModel;