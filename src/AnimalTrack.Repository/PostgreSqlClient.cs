using AnimalTrack.Repository.Interfaces;
using Npgsql;

namespace AnimalTrack.Repository;

public class PostgreSqlClient(IPostgreSqlConnectionFactory connectionFactory)
    : IPostgreSqlClient
{
    public async Task<List<Dictionary<string, object>>> RunReturningInsert(
        string query,
        IReadOnlyDictionary<string, object> parameters,
        IReadOnlyCollection<string> returnedColumns,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(query, nameof(query));
        ArgumentNullException.ThrowIfNull(parameters, nameof(parameters));
        ArgumentNullException.ThrowIfNull(returnedColumns, nameof(returnedColumns));
        
        await using var connection = await GetOpenConnection(cancellationToken);
        await using var command = CreateCommand(connection, query, parameters);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);

        return await IterateReader(reader, returnedColumns, cancellationToken);
    }

    public async Task<List<Dictionary<string, object>>> RunQuery(
        string query,
        IReadOnlyDictionary<string, object> parameters,
        IReadOnlyCollection<string> returnedColumns,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(query, nameof(query));
        ArgumentNullException.ThrowIfNull(parameters, nameof(parameters));
        ArgumentNullException.ThrowIfNull(returnedColumns, nameof(returnedColumns));
        
        await using var connection = await GetOpenConnection(cancellationToken);
        await using var command = CreateCommand(connection, query, parameters);
        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        
        return await IterateReader(reader, returnedColumns, cancellationToken);
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

    private static async Task<List<Dictionary<string, object>>> IterateReader(
        NpgsqlDataReader reader,
        IReadOnlyCollection<string> columns,
        CancellationToken cancellationToken = default)
    {
        var returnedRows = new List<Dictionary<string, object>>();
        while (await reader.ReadAsync(cancellationToken))
        {
            var record = new Dictionary<string, object>();
            foreach (var column in columns)
                record[column] = reader[column];
            returnedRows.Add(record);
        }
        return returnedRows;
    }
}