using AnimalTrack.Repository.Entities;
using AnimalTrack.Repository.Interfaces.Queries;

namespace AnimalTrack.Repository.Queries;

public class InsertAnimalSqlSelectQuery(string name) : ISqlSelectQuery<AnimalEntity>
{
    public string SqlText { get; } = """
                                     insert into Animals (Name)
                                        values (@Name)
                                        returning Id, Name, CreatedAt;
                                     """;
    public object? Parameters => new { Name = name };
}