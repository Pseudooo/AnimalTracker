using AnimalTrack.ClientModels.Interfaces;

namespace AnimalTrack.ClientModels.Models.Animals;

public class AnimalNoteModel : IIdentifiableModel, IDatedModel
{
    public int Id { get; set; }
    public required string Note { get; set; }
    public DateTime CreatedAt { get; set; }
}