using AnimalTrack.ClientModels.Models.Animals;

namespace AnimalTrack.Services.Requests.Queries;

public record GetAnimalTasksQuery(int AnimalId) : IQuery<List<AnimalTaskModel>>;