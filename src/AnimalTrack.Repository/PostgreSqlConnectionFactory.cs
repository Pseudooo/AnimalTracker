using AnimalTrack.Repository.Configuration;
using AnimalTrack.Repository.Interfaces;
using Microsoft.Extensions.Options;
using Npgsql;

namespace AnimalTrack.Repository;

public class PostgreSqlConnectionFactory(IOptions<DatabaseConfiguration> databaseConfiguration) : IPostgreSqlConnectionFactory
{
    public NpgsqlConnection GetConnection()
    {
        var connectionStringBuilder = new NpgsqlConnectionStringBuilder
        {
            Host = databaseConfiguration.Value.Host,
            Database = databaseConfiguration.Value.Database,
            Username = databaseConfiguration.Value.User,
            Password = databaseConfiguration.Value.Password,
#if DEBUG
            IncludeErrorDetail = true
#endif
        };
        var connectionString = connectionStringBuilder.ConnectionString;
        return new NpgsqlConnection(connectionString);
    }
}