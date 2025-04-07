using AnimalTrack.ClientModels.Constants;
using AnimalTrack.ClientModels.Interfaces.Animal;

namespace AnimalTrack.Services.Requests.Commands;

public record UpdateAnimalTaskCommand(
    int AnimalTaskId,
    string Name,
    SchedulingFrequency Frequency)
    : ICommand<bool>, IAnimalTaskModel;