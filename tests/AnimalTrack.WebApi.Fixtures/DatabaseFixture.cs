using System.Collections.Immutable;
using AnimalTrack.Configuration;
using AnimalTrack.Migrations;
using Npgsql;
using Testcontainers.PostgreSql;

namespace AnimalTrack.WebApi.Fixtures;

public class DatabaseFixture(params string[] databaseSeedScripts) : IAsyncDisposable
{
    private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder()
        .Build();

    private readonly ImmutableArray<string> _databaseSeedScripts = [..databaseSeedScripts];

    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        await _postgreSqlContainer.StartAsync(cancellationToken);

        var databaseConfig = GetDatabaseConfiguration();
        AnimalTrackMigrationRunner.Run(databaseConfig);

        var connectionString = new NpgsqlConnectionStringBuilder()
            {
                Host = databaseConfig.Host,
                Port = databaseConfig.Port,
                Database = databaseConfig.Database,
                Username = databaseConfig.Username,
                Password = databaseConfig.Password,
            }
            .ToString();
        await using var connection = new NpgsqlConnection(connectionString);
        await connection.OpenAsync(cancellationToken);
        
        foreach (var seedScript in _databaseSeedScripts)
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