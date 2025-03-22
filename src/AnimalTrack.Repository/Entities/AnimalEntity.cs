namespace AnimalTrack.Repository.Entities;

public class AnimalEntity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public DateTime CreatedAt { get; set; }
}