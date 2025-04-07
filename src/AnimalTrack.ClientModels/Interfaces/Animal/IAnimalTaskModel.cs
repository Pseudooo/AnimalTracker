using AnimalTrack.ClientModels.Constants;

namespace AnimalTrack.ClientModels.Interfaces.Animal;

public interface IAnimalTaskModel
{
    public string Name { get; }
    public SchedulingFrequency Frequency { get; }
    public DateOnly ScheduledFor { get; }
}