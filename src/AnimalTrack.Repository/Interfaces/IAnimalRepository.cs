using AnimalTrack.Repository.Entities;

namespace AnimalTrack.Repository.Interfaces;

public interface IAnimalRepository
{
    public Task<AnimalEntity> CreateAnimalAsync(string name, CancellationToken cancellationToken = default);
}