using AnimalTrack.ClientModels.Interfaces;
using AnimalTrack.ClientModels.Interfaces.Animal;

namespace AnimalTrack.ClientModels;

public class AnimalModel : IIdentifiableModel, IDatedModel, IAnimalModel
{
    public required int Id { get; init; }
    public required string Name { get; init; }
    public required DateTime CreatedAt { get; init; }
}