using Npgsql;

namespace AnimalTrack.Repository.Interfaces;

public interface IPostgreSqlConnectionFactory
{
    NpgsqlConnection GetConnection();
}