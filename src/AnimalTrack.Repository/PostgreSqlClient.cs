using AnimalTrack.Repository.Interfaces;
using AnimalTrack.Repository.Interfaces.Queries;
using Dapper;
using Npgsql;

namespace AnimalTrack.Repository;

public class PostgreSqlClient(IPostgreSqlConnectionFactory connectionFactory)
    : IPostgreSqlClient
{
    public async Task<T> InsertEntity<T>(IInsertSqlCommand<T> command, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));

        await using var connection = await GetOpenConnection(cancellationToken);  
        
        var commandDefinition = new CommandDefinition(command.SqlText, command.Parameters, cancellationToken: cancellationToken);
        return await connection.QuerySingleAsync<T>(commandDefinition);
    }
    
    public async Task<T?> RunSingleResultQuery<T>(
        ISqlSelectQuery<T> selectQuery,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(selectQuery, nameof(selectQuery));

        await using var connection = await GetOpenConnection(cancellationToken);
        
        var commandDefinition = new CommandDefinition(selectQuery.SqlText, selectQuery.Parameters, cancellationToken: cancellationToken);
        return await connection.QuerySingleOrDefaultAsync<T>(commandDefinition);
    }

    public async Task<List<T>> RunMultiResultQuery<T>(
        ISqlSelectQuery<T> selectQuery,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(selectQuery, nameof(selectQuery));

        await using var connection = await GetOpenConnection(cancellationToken);
        
        var commandDefinition = new CommandDefinition(selectQuery.SqlText, selectQuery.Parameters, cancellationToken: cancellationToken);
        var results = await connection.QueryAsync<T>(commandDefinition);
        return results.ToList();
    }

    public async Task<int> RunDeleteCommand(ISqlDeleteCommand command, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(command, nameof(command));
        
        await using var connection = await GetOpenConnection(cancellationToken);
        var commandDefinition = new CommandDefinition(command.SqlText, command.Parameters, cancellationToken: cancellationToken);
        var updatedRows = await connection.ExecuteAsync(commandDefinition);
        return updatedRows;
    }

    private async Task<NpgsqlConnection> GetOpenConnection(CancellationToken cancellationToken)
    {
        var connection = connectionFactory.GetConnection();
        await connection.OpenAsync(cancellationToken);
        return connection;
    }

    private static NpgsqlCommand CreateCommand(
        NpgsqlConnection connection,
        string commandText,
        IReadOnlyDictionary<string, object> parameters)
    {
        var command = connection.CreateCommand();
        command.CommandText = commandText;
        
        foreach(var (key, value) in parameters) 
            command.Parameters.AddWithValue(key, value);
        return command;
    }
}