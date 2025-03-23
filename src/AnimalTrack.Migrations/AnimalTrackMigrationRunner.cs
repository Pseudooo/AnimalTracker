using System.Reflection;
using AnimalTrack.Configuration;
using DbUp;
using DbUp.Engine;
using Npgsql;

namespace AnimalTrack.Migrations;

public static class AnimalTrackMigrationRunner
{
    public static DatabaseUpgradeResult Run(DatabaseConfiguration configuration)
    {
        var connectionStringBuilder = new NpgsqlConnectionStringBuilder()
        {
            Host = configuration.Host,
            Port = configuration.Port,
            Database = configuration.Database,
            Username = configuration.Username,
            Password = configuration.Password,
        };
        var connectionString = connectionStringBuilder.ToString();
        
        EnsureDatabase.For.PostgresqlDatabase(connectionString);
        var upgrader = DeployChanges.To
            .PostgresqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogToConsole()
            .Build();
        var result = upgrader.PerformUpgrade();
        
        return result;
    }
}