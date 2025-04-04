using AnimalTrack.Repository.Entities;
using AnimalTrack.Repository.Interfaces;

namespace AnimalTrack.Repository.Queries;

public class InsertAnimalSqlQuery(string name) : ITypedSqlQuery<AnimalEntity>
{
    public string SqlText { get; } = """
                                     insert into Animals (Name)
                                        values (@Name)
                                        returning Id, Name, CreatedAt;
                                     """;
    public object? Parameters => new { Name = name };
}