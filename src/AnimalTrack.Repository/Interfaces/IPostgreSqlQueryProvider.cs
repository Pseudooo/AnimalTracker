namespace AnimalTrack.Repository.Interfaces;

public interface IPostgreSqlQueryProvider
{
    public Task<string> GetInsertAnimalSqlText();
    Task<string> GetAnimalByIdSqlText();
    Task<string> GetAnimalPageSqlText();
    Task<string> GetUpdateAnimalSqlText();
}