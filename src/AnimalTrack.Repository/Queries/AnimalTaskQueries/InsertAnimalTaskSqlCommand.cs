using AnimalTrack.Repository.Entities;
using AnimalTrack.Repository.Interfaces.Queries;

namespace AnimalTrack.Repository.Queries.AnimalTaskQueries;

public class InsertAnimalTaskSqlCommand(int animalId, string name, string frequency) : IInsertSqlCommand<AnimalTaskEntity>
{
    public string SqlText { get; } = """
                                     insert into AnimalTasks (AnimalId, Name, Frequency)
                                        values (@AnimalId, @Name, @Frequency)
                                        returning Id, AnimalId, Name, Frequency, CreatedAt
                                     """;
    public object? Parameters => new { AnimalId = animalId, Name = name, Frequency = frequency };
}