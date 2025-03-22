using AnimalTrack.Repository.Interfaces;

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
        await using var connection = connectionFactory.GetConnection();
        await connection.OpenAsync(cancellationToken);
        
        await using var command = connection.CreateCommand();
        command.CommandText = query;
        
        foreach(var (key, value) in parameters) 
            command.Parameters.AddWithValue(key, value);

        await using var reader = await command.ExecuteReaderAsync(cancellationToken);
        
        var returnedRows = new List<Dictionary<string, object>>();
        while (await reader.ReadAsync(cancellationToken))
        {
            var record = new Dictionary<string, object>();
            foreach (var column in returnedColumns)
                record[column] = reader[column];
            returnedRows.Add(record);
        }
        
        return returnedRows;
    }
}