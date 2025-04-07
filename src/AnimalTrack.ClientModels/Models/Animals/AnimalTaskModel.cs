using AnimalTrack.ClientModels.Constants;
using AnimalTrack.ClientModels.Interfaces.Animal;

namespace AnimalTrack.ClientModels.Models.Animals;

public class AnimalTaskModel : IAnimalTaskModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public required SchedulingFrequency Frequency { get; set; }
    public DateOnly ScheduledFor { get; set; }
}