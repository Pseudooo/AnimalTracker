using System.Reflection;
using AnimalTrack.Repository.Interfaces;

namespace AnimalTrack.Repository.Providers;

public class PostgreSqlQueryProvider : IPostgreSqlQueryProvider
{
    private static readonly Lazy<string> QueryDirectoryPathLazy = new(
        () => Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Queries"));
    
    private static readonly Lazy<Task<string>> InsertAnimalQueryLazy = new(
        async () => await File.ReadAllTextAsync($"{QueryDirectoryPathLazy.Value}/insert_animal.sql"));
    
    public async Task<string> GetInsertAnimalSqlText() => await InsertAnimalQueryLazy.Value;
}