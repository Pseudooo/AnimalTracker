using AnimalTrack.Repository.Entities;
using AnimalTrack.Repository.Interfaces;

namespace AnimalTrack.Repository.Repositories;

public class AnimalRepository(IPostgreSqlQueryProvider provider, IPostgreSqlClient sqlClient)
    : IAnimalRepository
{
    public async Task<AnimalEntity> InsertAnimal(string name, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(name, nameof(name));

        var parameters = new Dictionary<string, object>()
        {
            { "@Name", name }
        };
        var query = await provider.GetInsertAnimalSqlText();
        List<string> returnedColumns =
        [
            "Id", "CreatedAt"
        ];
        
        var insertedRows = await sqlClient.RunReturningInsert(
            query,
            parameters,
            returnedColumns,
            cancellationToken);
        var insertedRow = insertedRows.Single();

        var animalEntity = new AnimalEntity()
        {
            Id = insertedRow["Id"] as int? ?? throw new InvalidDataException(nameof(insertedRow)),
            Name = name,
            CreatedAt = insertedRow["CreatedAt"] as DateTime? ?? throw new InvalidDataException(nameof(insertedRow)),
        };

        return animalEntity;
    }

    public async Task<AnimalNoteEntity> InsertAnimalNote(int animalId, string note, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(note, nameof(note));

        var parameters = new Dictionary<string, object>()
        {
            { "@AnimalId", animalId },
            { "@Note", note },
        };
        var query = await provider.GetInsertAnimalNoteSqlText();
        List<string> returnedColumns =
        [
            "Id", "CreatedAt"
        ];
        
        var insertedRows = await sqlClient.RunReturningInsert(
            query,
            parameters,
            returnedColumns,
            cancellationToken);
        var insertedRow = insertedRows.Single();
        
        return new AnimalNoteEntity()
        {
            Id = insertedRow["Id"] as int? ?? throw new InvalidDataException(nameof(insertedRow)),
            AnimalId = animalId,
            Note = note,
            CreatedAt = insertedRow["CreatedAt"] as DateTime? ?? throw new InvalidDataException(nameof(insertedRow)),
        };
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

    public async Task<bool> UpdateAnimal(int animalId, string name, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(animalId, nameof(animalId));

        var parameters = new Dictionary<string, object>()
        {
            { "@Id", animalId },
            { "@Name", name }
        };
        var query = await provider.GetUpdateAnimalSqlText();

        var updated = await sqlClient.RunNonQuery(
            query,
            parameters,
            cancellationToken);
        
        return updated == 1;
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