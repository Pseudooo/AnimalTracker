using AnimalTrack.Repository.Entities;
using AnimalTrack.Repository.Interfaces;

namespace AnimalTrack.Repository.Repositories;

public class AnimalRepository(IPostgreSqlQueryProvider provider, IPostgreSqlClient sqlClient)
    : IAnimalRepository
{
    public async Task<AnimalEntity> InsertAnimal(string name, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(name, nameof(name));

        var query = await provider.GetInsertAnimalSqlText();
        var parameters = new
        {
            Name = name,
        };
        var result = await sqlClient.InsertSingle<AnimalEntity>(query, parameters, cancellationToken);
        return result;
    }

    public async Task<AnimalNoteEntity> InsertAnimalNote(int animalId, string note, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(note, nameof(note));
        
        var query = await provider.GetInsertAnimalNoteSqlText();
        var parameters = new
        {
            AnimalId = animalId,
            Note = note,
        };
        var result = await sqlClient.InsertSingle<AnimalNoteEntity>(query, parameters, cancellationToken);
        return result;
    }

    public async Task<AnimalTaskEntity> InsertAnimalTask(
        int animalId,
        string name,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(name, nameof(name));

        var query = await provider.GetInsertAnimalTaskSqlText();
        var parameters = new
        {
            AnimalId = animalId,
            Name = name,
        };
        var result = await sqlClient.InsertSingle<AnimalTaskEntity>(query, parameters, cancellationToken);
        return result;
    }

    public async Task<AnimalEntity?> GetAnimalById(int id, CancellationToken cancellationToken = default)
    {
        var query = await provider.GetAnimalByIdSqlText();
        return await sqlClient.RunSingleResultQuery<AnimalEntity>(query, new { Id = id }, cancellationToken);
    }

    public async Task<List<AnimalNoteEntity>> GetAnimalNotes(
        int animalId,
        CancellationToken cancellationToken = default)
    {
        var query = await provider.GetAnimalNotesSqlText();
        var parameters = new
        {
            AnimalId = animalId,
        };
        return await sqlClient.RunMultiResultQuery<AnimalNoteEntity>(query, parameters, cancellationToken);
    }

    public async Task<List<AnimalEntity>> GetAnimalPage(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var query = await provider.GetAnimalPageSqlText();
        var parameters = new
        {
            Skip = (pageNumber - 1) * pageSize,
            Take = pageSize,
        };
        return await sqlClient.RunMultiResultQuery<AnimalEntity>(query, parameters, cancellationToken);
    }

    public async Task<List<AnimalTaskEntity>> GetAnimalTasks(
        int animalId,
        CancellationToken cancellationToken = default)
    {
        var query = await provider.GetAnimalTasksSqlText();
        var parameters = new
        {
            AnimalId = animalId,
        };
        return await sqlClient.RunMultiResultQuery<AnimalTaskEntity>(query, parameters, cancellationToken);
    }

    public async Task<bool> UpdateAnimal(int animalId, string name, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(animalId, nameof(animalId));

        var query = await provider.GetUpdateAnimalSqlText();
        var parameters = new
        {
            Id = animalId,
            Name = name,
        };
        var updatedAnimal = await sqlClient.UpdateSingle<AnimalEntity>(query, parameters, cancellationToken);
        
        return updatedAnimal is not null;
    }

    public async Task<bool> UpdateAnimalTask(
        int animalTaskId,
        string name,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(name);
        
        var query = await provider.GetUpdateAnimalTaskSqlText();
        var parameters = new
        {
            TaskId = animalTaskId,
            Name = name,
        };
        var updatedAnimal = await sqlClient.UpdateSingle<AnimalTaskEntity>(query, parameters, cancellationToken);
        
        return updatedAnimal is not null;
    }

    public async Task<bool> DeleteAnimalNote(int noteId, CancellationToken cancellationToken = default)
    {
        var parameters = new Dictionary<string, object>()
        {
            { "@Id", noteId }
        };
        var query = await provider.DeleteAnimalNoteSqlText();
        
        var deleted = await sqlClient.RunNonQuery(
            query,
            parameters,
            cancellationToken);
        return deleted == 1;
    }
}