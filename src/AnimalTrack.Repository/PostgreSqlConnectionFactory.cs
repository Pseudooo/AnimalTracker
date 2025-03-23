using AnimalTrack.Configuration;
using AnimalTrack.Repository.Interfaces;
using Npgsql;

namespace AnimalTrack.Repository;

public class PostgreSqlConnectionFactory(DatabaseConfiguration databaseConfiguration) : IPostgreSqlConnectionFactory
{
    public NpgsqlConnection GetConnection()
    {
        var connectionStringBuilder = new NpgsqlConnectionStringBuilder
        {
            Host = databaseConfiguration.Host,
            Port = databaseConfiguration.Port,
            Database = databaseConfiguration.Database,
            Username = databaseConfiguration.Username,
            Password = databaseConfiguration.Password,
#if DEBUG
            IncludeErrorDetail = true
#endif
        };
        var connectionString = connectionStringBuilder.ConnectionString;
        return new NpgsqlConnection(connectionString);
    }
}