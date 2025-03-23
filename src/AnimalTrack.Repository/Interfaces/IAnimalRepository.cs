using AnimalTrack.Repository.Entities;

namespace AnimalTrack.Repository.Interfaces;

public interface IAnimalRepository
{
    public Task<AnimalEntity> InsertAnimal(string name, CancellationToken cancellationToken = default);
    Task<AnimalEntity?> GetAnimalById(int id, CancellationToken cancellationToken = default);
    Task<List<AnimalEntity>> GetAnimalPage(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default);
}