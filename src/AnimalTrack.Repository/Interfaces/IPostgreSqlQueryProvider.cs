namespace AnimalTrack.Repository.Interfaces;

public interface IPostgreSqlQueryProvider
{
    public Task<string> GetInsertAnimalSqlText();
    Task<string> GetAnimalByIdSqlText();
    Task<string> GetAnimalPageSqlText();
    Task<string> GetUpdateAnimalSqlText();
    Task<string> GetInsertAnimalNoteSqlText();
    Task<string> GetAnimalNotesSqlText();
    Task<string> DeleteAnimalNoteSqlText();
    Task<string> GetAnimalTasksSqlText();
    Task<string> GetInsertAnimalTaskSqlText();
    Task<string> GetUpdateAnimalTaskSqlText();
}