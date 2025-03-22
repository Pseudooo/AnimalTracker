using System.Data.Common;

namespace AnimalTrack.Repository.Interfaces;

public interface IPostgreSqlConnectionFactory
{
    DbConnection GetConnection();
}