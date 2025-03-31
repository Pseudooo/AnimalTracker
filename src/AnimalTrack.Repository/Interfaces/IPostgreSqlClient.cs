namespace AnimalTrack.Repository.Interfaces;

public interface IPostgreSqlClient
{
    Task<T?> RunSingleResultQuery<T>(
        string queryText,
        object? parameters,
        CancellationToken cancellationToken = default);

    Task<List<T>> RunMultiResultQuery<T>(
        string queryText,
        object? parameters,
        CancellationToken cancellationToken = default);
    
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
    
    Task<int> RunNonQuery(
        string query,
        IReadOnlyDictionary<string, object> parameters,
        CancellationToken cancellationToken = default);
}