namespace AnimalTrack.Repository.Entities;

public class AnimalNoteEntity
{
    public int Id { get; set; }
    public int AnimalId { get; set; }
    public required string Note { get; set; }
    public DateTime CreatedAt { get; set; }
}