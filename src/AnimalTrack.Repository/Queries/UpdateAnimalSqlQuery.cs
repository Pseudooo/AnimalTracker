using AnimalTrack.Repository.Entities;
using AnimalTrack.Repository.Interfaces;

namespace AnimalTrack.Repository.Queries;

public class UpdateAnimalSqlQuery(int animalId, string name) : ITypedSqlQuery<AnimalEntity>
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