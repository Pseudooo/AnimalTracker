using AnimalTrack.Configuration;
using AnimalTrack.Migrations;
using Npgsql;
using Testcontainers.PostgreSql;

namespace AnimalTrack.WebApi.Tests.Fixtures;

public class DatabaseFixture : IAsyncDisposable
{
    private readonly PostgreSqlContainer _postgreSqlContainer = new PostgreSqlBuilder()
        .Build();

    public async Task StartAsync(CancellationToken cancellationToken = default)
    {
        await _postgreSqlContainer.StartAsync(cancellationToken);
        AnimalTrackMigrationRunner.Run(GetDatabaseConfiguration());
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