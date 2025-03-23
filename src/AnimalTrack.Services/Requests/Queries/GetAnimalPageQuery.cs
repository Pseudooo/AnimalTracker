using AnimalTrack.ClientModels;

namespace AnimalTrack.Services.Requests.Queries;

public record GetAnimalPageQuery(int PageNumber = 1, int PageSize = 30) : IQuery<List<AnimalModel>>;