using AnimalTrack.Repository.Entities;
using AnimalTrack.Repository.Interfaces;

namespace AnimalTrack.Repository.Repositories;

public class AnimalRepository(IPostgreSqlQueryProvider provider, IPostgreSqlClient sqlClient)
    : IAnimalRepository
{
    public async Task<AnimalEntity> CreateAnimalAsync(string name, CancellationToken cancellationToken = default)
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
}