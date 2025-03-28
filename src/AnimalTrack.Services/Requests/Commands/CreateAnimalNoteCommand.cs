using AnimalTrack.ClientModels.Models.Animals;

namespace AnimalTrack.Services.Requests.Commands;

public record CreateAnimalNoteCommand(int AnimalId, string Note) : ICommand<AnimalNoteModel>;