using AnimalTrack.ClientModels.Constants;
using AnimalTrack.ClientModels.Interfaces.Animal;

namespace AnimalTrack.WebApi.Requests;

public class CreateAnimalTaskRequestBody : IAnimalTaskModel
{
    public required string Name { get; set;  }
    public required SchedulingFrequency Frequency { get; set; }
}