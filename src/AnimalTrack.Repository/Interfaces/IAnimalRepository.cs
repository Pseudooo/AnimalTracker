using AnimalTrack.Repository.Entities;

namespace AnimalTrack.Repository.Interfaces;

public interface IAnimalRepository
{
    public Task<AnimalEntity> InsertAnimal(string name, CancellationToken cancellationToken = default);
    
    public Task<AnimalNoteEntity> InsertAnimalNote(
        int animalId,
        string note,
        CancellationToken cancellationToken = default);

    Task<AnimalTaskEntity> InsertAnimalTask(
        int animalId,
        string name,
        CancellationToken cancellationToken = default);
    
    Task<AnimalEntity?> GetAnimalById(int id, CancellationToken cancellationToken = default);
    
    Task<List<AnimalNoteEntity>> GetAnimalNotes(
        int animalId,
        CancellationToken cancellationToken = default);

    Task<List<AnimalEntity>> GetAnimalPage(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default);

    Task<List<AnimalTaskEntity>> GetAnimalTasks(
        int animalId,
        CancellationToken cancellationToken = default);
    
    Task<bool> UpdateAnimal(int animalId, string name, CancellationToken cancellationToken = default);

    Task<bool> UpdateAnimalTask(int animalTaskId, string name, CancellationToken cancellationToken = default);
    
    Task<bool> DeleteAnimalNote(int noteId, CancellationToken cancellationToken = default);
}