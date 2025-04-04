using AnimalTrack.Repository.Entities;
using AnimalTrack.Repository.Interfaces.Queries;

namespace AnimalTrack.Repository.Queries;

public class UpdateAnimalSqlSelectQuery(int animalId, string name) : ISqlSelectQuery<AnimalEntity>
{
    public string SqlText { get; } = """
                                     update Animals
                                        set
                                            Name = @Name
                                        where Id = @AnimalId
                                        returning Id, Name, CreatedAt
                                     """;

    public object? Parameters => new { AnimalId = animalId, Name = name };
}