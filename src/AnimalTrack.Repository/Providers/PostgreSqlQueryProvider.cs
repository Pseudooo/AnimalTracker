using System.Reflection;
using AnimalTrack.Repository.Interfaces;

namespace AnimalTrack.Repository.Providers;

public class PostgreSqlQueryProvider : IPostgreSqlQueryProvider
{
    private static readonly Lazy<string> QueryDirectoryPathLazy = new(
        () => Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "Queries"));
    
    private static readonly Lazy<Task<string>> InsertAnimalQueryLazy = new(
        async () => await File.ReadAllTextAsync($"{QueryDirectoryPathLazy.Value}/insert_animal.sql"));
    
    private static readonly Lazy<Task<string>> InsertAnimalNoteQueryLazy = new(
        async () => await File.ReadAllTextAsync($"{QueryDirectoryPathLazy.Value}/insert_animal_note.sql"));
    
    private static readonly Lazy<Task<string>> DeleteAnimalNoteQueryLazy = new(
        async () => await File.ReadAllTextAsync($"{QueryDirectoryPathLazy.Value}/delete_animal_note.sql"));
    
    private static readonly Lazy<Task<string>> InsertAnimalTaskQueryLazy = new(
        async () => await File.ReadAllTextAsync($"{QueryDirectoryPathLazy.Value}/AnimalTaskQueries/insert_animal_task.sql"));
    
    public async Task<string> GetInsertAnimalSqlText() => await InsertAnimalQueryLazy.Value;
    public async Task<string> GetInsertAnimalNoteSqlText() => await InsertAnimalNoteQueryLazy.Value;
    public async Task<string> DeleteAnimalNoteSqlText() => await DeleteAnimalNoteQueryLazy.Value;
    public async Task<string> GetInsertAnimalTaskSqlText() => await InsertAnimalTaskQueryLazy.Value;
}