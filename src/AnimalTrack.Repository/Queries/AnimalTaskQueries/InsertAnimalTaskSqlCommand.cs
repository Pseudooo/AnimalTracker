using AnimalTrack.Repository.Entities;
using AnimalTrack.Repository.Interfaces.Queries;

namespace AnimalTrack.Repository.Queries.AnimalTaskQueries;

public class InsertAnimalTaskSqlCommand(int animalId, string name) : IInsertSqlCommand<AnimalTaskEntity>
{
    public string SqlText { get; } = """
                                     insert into AnimalTasks (AnimalId, Name)
                                        values (@AnimalId, @Name)
                                        returning Id, AnimalId, Name, CreatedAt
                                     """;
    public object? Parameters => new { AnimalId = animalId, Name = name };
}