using System.Reflection;
using AnimalTrack.Repository.Interfaces;

namespace AnimalTrack.Repository.Providers;

public class PostgreSqlQueryProvider : IPostgreSqlQueryProvider
{
    private static readonly Lazy<string> QueryDirectoryPathLazy = new(
        () => Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Queries"));
    
    private static readonly Lazy<Task<string>> InsertAnimalQueryLazy = new(
        async () => await File.ReadAllTextAsync($"{QueryDirectoryPathLazy.Value}/insert_animal.sql"));
    
    private static readonly Lazy<Task<string>> GetAnimalByIdQueryLazy = new(
        async () => await File.ReadAllTextAsync($"{QueryDirectoryPathLazy.Value}/get_animal_by_id.sql"));
    
    private static readonly Lazy<Task<string>> GetAnimalPageQuery = new(
        async () => await File.ReadAllTextAsync($"{QueryDirectoryPathLazy.Value}/get_animal_page.sql"));

    private static readonly Lazy<Task<string>> GetAnimalUpdateQuery = new(
        async () => await File.ReadAllTextAsync($"{QueryDirectoryPathLazy.Value}/update_animal.sql"));
    
    public async Task<string> GetInsertAnimalSqlText() => await InsertAnimalQueryLazy.Value;
    
    public async Task<string> GetAnimalByIdSqlText() => await GetAnimalByIdQueryLazy.Value;
    
    public async Task<string> GetAnimalPageSqlText() => await GetAnimalPageQuery.Value;
    
    public async Task<string> GetUpdateAnimalSqlText() => await GetAnimalUpdateQuery.Value;
}