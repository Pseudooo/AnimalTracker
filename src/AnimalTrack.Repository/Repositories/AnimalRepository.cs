using AnimalTrack.Repository.Entities;
using AnimalTrack.Repository.Interfaces;
using AnimalTrack.Repository.Queries;
using AnimalTrack.Repository.Queries.AnimalNoteQueries;
using AnimalTrack.Repository.Queries.AnimalTaskQueries;

namespace AnimalTrack.Repository.Repositories;

public class AnimalRepository(IPostgreSqlClient sqlClient)
    : IAnimalRepository
{
    public async Task<AnimalEntity> InsertAnimal(string name, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(name, nameof(name));

        var query = new InsertAnimalSqlCommand(name);
        return await sqlClient.InsertEntity(query, cancellationToken);
    }

    public async Task<AnimalNoteEntity> InsertAnimalNote(int animalId, string note, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(note, nameof(note));
        
        var command = new InsertAnimalNoteSqlCommand(animalId, note);
        return await sqlClient.InsertEntity(command, cancellationToken);
    }

    public async Task<AnimalTaskEntity> InsertAnimalTask(
        int animalId,
        string name,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(name, nameof(name));

        var query = new InsertAnimalTaskSqlCommand(animalId, name);
        return await sqlClient.InsertEntity(query, cancellationToken);
    }

    public async Task<AnimalEntity?> GetAnimalById(int id, CancellationToken cancellationToken = default)
    {
        var query = new GetAnimalByIdSqlSelectQuery(id);
        return await sqlClient.RunSingleResultQuery(query, cancellationToken);
    }

    public async Task<List<AnimalNoteEntity>> GetAnimalNotes(
        int animalId,
        CancellationToken cancellationToken = default)
    {
        var query = new GetAnimalNotesSqlSelectQuery(animalId);
        return await sqlClient.RunMultiResultQuery(query, cancellationToken);
    }

    public async Task<List<AnimalEntity>> GetAnimalPage(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var query = new GetAnimalPageSqlSelectQuery(pageSize, pageNumber);
        return await sqlClient.RunMultiResultQuery(query, cancellationToken);
    }

    public async Task<List<AnimalTaskEntity>> GetAnimalTasks(
        int animalId,
        CancellationToken cancellationToken = default)
    {
        var query = new GetAnimalTasksSqlSelectQuery(animalId);
        return await sqlClient.RunMultiResultQuery(query, cancellationToken);
    }

    public async Task<bool> UpdateAnimal(int animalId, string name, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(name, nameof(name));
        
        var query = new UpdateAnimalSqlSelectQuery(animalId, name);
        return await sqlClient.RunSingleResultQuery(query, cancellationToken) is not null;
    }

    public async Task<bool> UpdateAnimalTask(
        int animalTaskId,
        string name,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(name, nameof(name));
        
        var query = new UpdateAnimalTaskSqlSelectQuery(animalTaskId, name);
        return await sqlClient.RunSingleResultQuery(query, cancellationToken) is not null;
    }

    public async Task<bool> DeleteAnimalNote(int noteId, CancellationToken cancellationToken = default)
    {
        var command = new DeleteAnimalNoteSqlCommand(noteId);
        var deletedCount = await sqlClient.RunDeleteCommand(command, cancellationToken);
        return deletedCount == 1;
    }
}