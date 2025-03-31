using AnimalTrack.Repository.Interfaces;
using Dapper;
using Npgsql;

namespace AnimalTrack.Repository;

public class PostgreSqlClient(IPostgreSqlConnectionFactory connectionFactory)
    : IPostgreSqlClient
{
    public async Task<T> InsertSingle<T>(string query, object? parameters, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(query, nameof(query));

        await using var connection = await GetOpenConnection(cancellationToken);
        var commandDefinition = new CommandDefinition(query, parameters, cancellationToken: cancellationToken);
        return await connection.QuerySingleAsync<T>(commandDefinition);
    }
    
    public async Task<T?> RunSingleResultQuery<T>(
        string queryText,
        object? parameters,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(queryText, nameof(queryText));
        
        await using var connection = await GetOpenConnection(cancellationToken);
        
        var commandDefinition = new CommandDefinition(queryText, parameters, cancellationToken: cancellationToken);
        return await connection.QuerySingleOrDefaultAsync<T>(commandDefinition);
    }

    public async Task<List<T>> RunMultiResultQuery<T>(
        string queryText,
        object? parameters,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(queryText, nameof(queryText));
        
        await using var connection = await GetOpenConnection(cancellationToken);
        
        var commandDefinition = new CommandDefinition(queryText, parameters, cancellationToken: cancellationToken);
        var results = await connection.QueryAsync<T>(commandDefinition);
        return results.ToList();
    }

    public async Task<T?> UpdateSingle<T>(
        string query,
        object? parameters,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(query, nameof(query));
        
        await using var connection = await GetOpenConnection(cancellationToken);
        
        var commandDefinition = new CommandDefinition(query, parameters, cancellationToken: cancellationToken);
        return await connection.QuerySingleOrDefaultAsync(commandDefinition);
    }

    public async Task<int> RunNonQuery(
        string query,
        IReadOnlyDictionary<string, object> parameters,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(query, nameof(query));
        ArgumentNullException.ThrowIfNull(parameters, nameof(parameters));
        
        await using var connection = await GetOpenConnection(cancellationToken);
        await using var command = CreateCommand(connection, query, parameters);
        var count = await command.ExecuteNonQueryAsync(cancellationToken);
        return count;
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