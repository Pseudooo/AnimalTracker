using AnimalTrack.ClientModels.Interfaces.Animal;

namespace AnimalTrack.ClientModels.Models.Animals;

public class AnimalTaskModel : IAnimalTaskModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public DateTime CreatedAt { get; set; }
}