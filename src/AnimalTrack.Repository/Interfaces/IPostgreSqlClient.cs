using AnimalTrack.Repository.Interfaces.Queries;

namespace AnimalTrack.Repository.Interfaces;

public interface IPostgreSqlClient
{
    Task<T> InsertEntity<T>(IInsertSqlCommand<T> command, CancellationToken cancellationToken = default);
    
    Task<T?> RunSingleResultQuery<T>(ISqlSelectQuery<T> selectQuery, CancellationToken cancellationToken = default);

    Task<List<T>> RunMultiResultQuery<T>(ISqlSelectQuery<T> selectQuery, CancellationToken cancellationToken = default);

    Task<int> RunDeleteCommand(ISqlDeleteCommand command, CancellationToken cancellationToken = default);
}