using AnimalTrack.Repository.Entities;
using AnimalTrack.Repository.Interfaces;

namespace AnimalTrack.Repository.Queries.AnimalTaskQueries;

public class InsertAnimalTaskSqlQuery(int animalId, string name) : ITypedSqlQuery<AnimalTaskEntity>
{
    public string SqlText { get; } = """
                                     insert into AnimalTasks (AnimalId, Name)
                                        values (@AnimalId, @Name)
                                        returning Id, AnimalId, Name, CreatedAt
                                     """;
    public object? Parameters => new { AnimalId = animalId, Name = name };
}