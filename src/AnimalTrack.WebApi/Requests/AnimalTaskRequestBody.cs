using AnimalTrack.ClientModels.Constants;
using AnimalTrack.ClientModels.Interfaces.Animal;

namespace AnimalTrack.WebApi.Requests;

public class AnimalTaskRequestBody : IAnimalTaskModel
{
    public required string Name { get; set;  }
    public required SchedulingFrequency Frequency { get; set; }
    public DateOnly ScheduledFor { get; set; }
}