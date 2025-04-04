namespace AnimalTrack.Repository.Interfaces;

public interface IPostgreSqlQueryProvider
{
    Task<string> DeleteAnimalNoteSqlText();
}