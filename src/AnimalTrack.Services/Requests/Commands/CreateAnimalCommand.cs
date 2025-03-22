using AnimalTrack.ClientModels;
using AnimalTrack.ClientModels.Interfaces.Animal;

namespace AnimalTrack.Services.Requests.Commands;

public record CreateAnimalCommand(string Name) : ICommand<AnimalModel>, IAnimalModel;