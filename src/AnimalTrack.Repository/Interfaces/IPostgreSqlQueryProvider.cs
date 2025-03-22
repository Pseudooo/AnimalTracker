namespace AnimalTrack.Repository.Interfaces;

public interface IPostgreSqlQueryProvider
{
    public Task<string> GetInsertAnimalSqlText();
    Task<string> GetAnimalByIdSqlText();
}