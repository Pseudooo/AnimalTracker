// See https://aka.ms/new-console-template for more information

using System.Reflection;
using DbUp;
using Microsoft.Extensions.Configuration;
using Npgsql;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

var connectionStringBuilder = new NpgsqlConnectionStringBuilder()
{
    Host = config["Host"],
    Port = int.Parse(config["Port"] ?? "5432"),
    Database = config["Database"],
    Username = config["User"],
    Password = config["Password"]
};

var connectionString = connectionStringBuilder.ConnectionString;

EnsureDatabase.For.PostgresqlDatabase(connectionString);
var upgrader = DeployChanges.To
    .PostgresqlDatabase(connectionString)
    .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
    .LogToConsole()
    .Build();

var result = upgrader.PerformUpgrade();

if (!result.Successful)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(result.Error);
    Console.ResetColor();
    return -1;
}

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Success!");
Console.ResetColor();
return 0;