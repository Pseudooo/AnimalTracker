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
        
        var animalEntity = new AnimalEntity()
        {
            Id = result["Id"] as int? ?? throw new InvalidDataException(nameof(result)),
            Name = result["Name"] as string ?? throw new InvalidDataException(nameof(result)),
            CreatedAt = result["CreatedAt"] as DateTime? ?? throw new InvalidDataException(nameof(result)),
        };
        return animalEntity;
    }
}