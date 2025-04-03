namespace AnimalTrack.Repository.Entities;

public class AnimalTaskEntity
{
    public int Id { get; set; }
    public int AnimalId { get; set; }
    public required string Name { get; set; }
    public DateTime CreatedAt { get; set; }
}