using AnimalTrack.ClientModels;

namespace AnimalTrack.Services.Requests.Queries;

public record GetAnimalByIdQuery(int Id) : IQuery<AnimalModel>;