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

    public async Task<AnimalEntity?> GetAnimalById(int id, CancellationToken cancellationToken = default)
    {
        var parameters = new Dictionary<string, object>()
        {
            { "@Id", id }
        };
        var query = await provider.GetAnimalByIdSqlText();
        List<string> returnedColumns =
        [
            "Id", "Name", "CreatedAt",
        ];
        
        var results = await sqlClient.RunQuery(
            query,
            parameters,
            returnedColumns,
            cancellationToken);
        var result = results.SingleOrDefault();
        if (result is null)
            return null;
        
        return MapAnimalEntityFromDictionary(result);
    }

    public async Task<List<AnimalEntity>> GetAnimalPage(
        int pageNumber,
        int pageSize,
        CancellationToken cancellationToken = default)
    {
        var rowsToSkip = (pageNumber - 1) * pageSize;
        var parameters = new Dictionary<string, object>()
        {
            { "@Skip", rowsToSkip },
            { "@Take", pageSize },
        };
        var query = await provider.GetAnimalPageSqlText();
        List<string> returnedColumns =
        [
            "Id", "Name", "CreatedAt"
        ];
        
        var results = await sqlClient.RunQuery(
            query,
            parameters,
            returnedColumns,
            cancellationToken);

        return MapAnimalEntitiesFromDictionaries(results);
    }

    private List<AnimalEntity> MapAnimalEntitiesFromDictionaries(
        IReadOnlyCollection<IReadOnlyDictionary<string, object>> animalEntities)
    {
        return animalEntities.Select(MapAnimalEntityFromDictionary)
            .ToList();
    }
    
    private AnimalEntity MapAnimalEntityFromDictionary(IReadOnlyDictionary<string, object> columnValues)
    {
        if(!columnValues.TryGetValue("Id", out object? idValue) || idValue is not int id)
            throw new InvalidDataException($"Unexpected value for {nameof(AnimalEntity)}.{nameof(AnimalEntity.Id)}");
        if(!columnValues.TryGetValue("Name", out object? nameValue) || nameValue is not string name)
            throw new InvalidDataException($"Unexpected value for {nameof(AnimalEntity)}.{nameof(AnimalEntity.Name)}");
        if(!columnValues.TryGetValue("CreatedAt", out object? createdAtValue) || createdAtValue is not DateTime createdAt)
            throw new InvalidDataException($"Unexpected format for {nameof(AnimalEntity)}.{nameof(AnimalEntity.CreatedAt)}");

        return new AnimalEntity
        {
            Id = id,
            Name = name,
            CreatedAt = createdAt,
        };
    }
}