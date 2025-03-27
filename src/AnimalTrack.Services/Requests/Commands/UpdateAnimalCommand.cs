using AnimalTrack.ClientModels.Interfaces.Animal;

namespace AnimalTrack.Services.Requests.Commands;

public record UpdateAnimalCommand(int Id, string Name) : ICommand<bool>, IAnimalModel;