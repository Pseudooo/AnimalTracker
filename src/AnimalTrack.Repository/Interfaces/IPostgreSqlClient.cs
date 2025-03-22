namespace AnimalTrack.Repository.Interfaces;

public interface IPostgreSqlClient
{
    Task<List<Dictionary<string, object>>> RunReturningInsert(
        string query,
        IReadOnlyDictionary<string, object> parameters,
        IReadOnlyCollection<string> returnedColumns,
        CancellationToken cancellationToken = default);
}