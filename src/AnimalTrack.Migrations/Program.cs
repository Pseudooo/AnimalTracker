// See https://aka.ms/new-console-template for more information

using AnimalTrack.Configuration;
using AnimalTrack.Migrations;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build()
    .Get<DatabaseConfiguration>();
if (config is null)
    throw new InvalidDataException("Invalid configuration");

var result = AnimalTrackMigrationRunner.Run(config);

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