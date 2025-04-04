using AnimalTrack.Repository;
using AnimalTrack.WebApi.Fixtures;
using Npgsql;

namespace AnimalTrack.WebApi.Tests.Extensions;

public static class DatabaseFixtureExtensions
{
    public static async Task AssertOnDatabaseReader(
        this DatabaseFixture databaseFixture,
        string tableName,
        int rowId,
        Action<NpgsqlDataReader> action,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(databaseFixture, nameof(databaseFixture));
        ArgumentNullException.ThrowIfNull(tableName, nameof(tableName));

        var connectionString = databaseFixture.GetDatabaseConfiguration()
            .GetConnectionString();
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);

        var query = $"""
                     select *
                        from {tableName}
                        where Id = @Id
                     """;
        await using var command = new NpgsqlCommand(query, connection);
        command.Parameters.AddWithValue("@Id", rowId);
        
        var reader = await command.ExecuteReaderAsync(cancellationToken);
        await reader.ReadAsync(cancellationToken);

        action(reader);
    }
}