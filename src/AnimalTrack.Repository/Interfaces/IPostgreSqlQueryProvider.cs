namespace AnimalTrack.Repository.Interfaces;

public interface IPostgreSqlQueryProvider
{
    public Task<string> GetInsertAnimalSqlText();
    Task<string> GetInsertAnimalNoteSqlText();
    Task<string> DeleteAnimalNoteSqlText();
    Task<string> GetInsertAnimalTaskSqlText();
}