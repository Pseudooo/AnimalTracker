using AnimalTrack.ClientModels.Constants;
using AnimalTrack.ClientModels.Interfaces.Animal;
using AnimalTrack.ClientModels.Models.Animals;

namespace AnimalTrack.Services.Requests.Commands;

public record CreateAnimalTaskCommand(
    int AnimalId,
    string Name,
    SchedulingFrequency Frequency,
    DateOnly ScheduledFor)
    : ICommand<AnimalTaskModel>, IAnimalTaskModel;