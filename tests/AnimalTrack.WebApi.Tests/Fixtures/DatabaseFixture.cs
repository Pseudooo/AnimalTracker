using AnimalTrack.Configuration;
using AnimalTrack.Migrations;
using AnimalTrack.Repository;
using Npgsql;
using Testcontainers.PostgreSql;

namespace AnimalTrack.WebApi.Tests.Fixtures;

public class DatabaseFixture(params List<string> databaseSeedScripts) : IAsyncDisposable
{
    private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder()
        .Build();

    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        await _postgreSqlContainer.StartAsync(cancellationToken);
        
        var databaseConfig = GetDatabaseConfiguration();
        AnimalTrackMigrationRunner.Run(databaseConfig);
        
        await using var connection = new NpgsqlConnection(databaseConfig.GetConnectionString());
        await connection.OpenAsync(cancellationToken);
        foreach (var seedScript in databaseSeedScripts)
        {
            await using var command = connection.CreateCommand();
            command.CommandText = seedScript;
            
            await command.ExecuteNonQueryAsync(cancellationToken);
        }
    }

    public async ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);
        await _postgreSqlContainer.DisposeAsync();
    }

    public DatabaseConfiguration GetDatabaseConfiguration()
    {
        var connectionString = _postgreSqlContainer.GetConnectionString();
        var builder = new NpgsqlConnectionStringBuilder(connectionString);
        return new DatabaseConfiguration()
        {
            Host = builder.Host!,
            Port = builder.Port,
            Database = builder.Database!,
            Username = builder.Username!,
            Password = builder.Password!,
        };
    }
}