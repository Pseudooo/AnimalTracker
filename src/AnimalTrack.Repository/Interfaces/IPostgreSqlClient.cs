namespace AnimalTrack.Repository.Interfaces;

public interface IPostgreSqlClient
{
    Task<T> InsertSingle<T>(string query, object? parameters, CancellationToken cancellationToken = default);
    
    Task<T?> RunSingleResultQuery<T>(
        string queryText,
        object? parameters,
        CancellationToken cancellationToken = default);

    Task<List<T>> RunMultiResultQuery<T>(
        string queryText,
        object? parameters,
        CancellationToken cancellationToken = default);

    Task<T?> UpdateSingle<T>(string query, object? parameters, CancellationToken cancellationToken = default);
    
    Task<int> RunNonQuery(
        string query,
        IReadOnlyDictionary<string, object> parameters,
        CancellationToken cancellationToken = default);
}