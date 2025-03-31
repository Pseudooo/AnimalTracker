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

    private static readonly Lazy<Task<string>> InsertAnimalNoteQueryLazy = new(
        async () => await File.ReadAllTextAsync($"{QueryDirectoryPathLazy.Value}/insert_animal_note.sql"));

    private static readonly Lazy<Task<string>> GetAnimalNotesQueryLazy = new(
        async () => await File.ReadAllTextAsync($"{QueryDirectoryPathLazy.Value}/get_animal_notes.sql"));
    
    private static readonly Lazy<Task<string>> DeleteAnimalNoteQueryLazy = new(
        async () => await File.ReadAllTextAsync($"{QueryDirectoryPathLazy.Value}/delete_animal_note.sql"));
    
    public async Task<string> GetInsertAnimalSqlText() => await InsertAnimalQueryLazy.Value;
    
    public async Task<string> GetAnimalByIdSqlText() => await GetAnimalByIdQueryLazy.Value;
    
    public async Task<string> GetAnimalPageSqlText() => await GetAnimalPageQuery.Value;
    
    public async Task<string> GetUpdateAnimalSqlText() => await GetAnimalUpdateQuery.Value;
    
    public async Task<string> GetInsertAnimalNoteSqlText() => await InsertAnimalNoteQueryLazy.Value;
    
    public async Task<string> GetAnimalNotesSqlText() => await GetAnimalNotesQueryLazy.Value;
    
    public async Task<string> DeleteAnimalNoteSqlText() => await DeleteAnimalNoteQueryLazy.Value;
}