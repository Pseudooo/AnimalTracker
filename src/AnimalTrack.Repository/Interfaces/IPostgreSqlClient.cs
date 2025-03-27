namespace AnimalTrack.Repository.Interfaces;

public interface IPostgreSqlClient
{
    Task<List<Dictionary<string, object>>> RunReturningInsert(
        string query,
        IReadOnlyDictionary<string, object> parameters,
        IReadOnlyCollection<string> returnedColumns,
        CancellationToken cancellationToken = default);

    Task<List<Dictionary<string, object>>> RunQuery(
        string query,
        IReadOnlyDictionary<string, object> parameters,
        IReadOnlyCollection<string> returnedColumns,
        CancellationToken cancellationToken = default);
    
    Task<int> RunUpdate(
        string query,
        IReadOnlyDictionary<string, object> parameters,
        CancellationToken cancellationToken = default);
}