using AnimalTrack.Configuration;
using Npgsql;

namespace AnimalTrack.Repository;

public static class DatabaseConfigurationExtensions
{
    public static string GetConnectionString(this DatabaseConfiguration databaseConfiguration)
    {
        var connectionStringBuilder = new NpgsqlConnectionStringBuilder()
        {
            Host = databaseConfiguration.Host,
            Port = databaseConfiguration.Port,
            Database = databaseConfiguration.Database,
            Username = databaseConfiguration.Username,
            Password = databaseConfiguration.Password,
        };
        return connectionStringBuilder.ToString();
    }
}