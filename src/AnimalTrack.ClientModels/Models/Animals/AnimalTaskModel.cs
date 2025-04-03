namespace AnimalTrack.ClientModels.Models.Animals;

public class AnimalTaskModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public DateTime CreatedAt { get; set; }
}