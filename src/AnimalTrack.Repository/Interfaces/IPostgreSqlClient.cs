namespace AnimalTrack.Repository.Interfaces;

public interface IPostgreSqlClient
{
    Task<T> InsertSingle<T>(string query, object? parameters, CancellationToken cancellationToken = default);

    Task<T?> RunSingleResultQuery<T>(ITypedSqlQuery<T> query, CancellationToken cancellationToken = default);

    Task<List<T>> RunMultiResultQuery<T>(ITypedSqlQuery<T> query, CancellationToken cancellationToken = default);

    Task<T?> UpdateSingle<T>(string query, object? parameters, CancellationToken cancellationToken = default);
    
    Task<int> RunNonQuery(
        string query,
        IReadOnlyDictionary<string, object> parameters,
        CancellationToken cancellationToken = default);
}