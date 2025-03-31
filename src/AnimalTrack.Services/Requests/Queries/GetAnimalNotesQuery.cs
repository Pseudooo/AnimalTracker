using AnimalTrack.ClientModels.Models.Animals;

namespace AnimalTrack.Services.Requests.Queries;

public record GetAnimalNotesQuery(int AnimalId) : IQuery<List<AnimalNoteModel>>;