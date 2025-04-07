using AnimalTrack.Repository.Entities;
using AnimalTrack.Repository.Interfaces.Queries;

namespace AnimalTrack.Repository.Queries.AnimalTaskQueries;

public class InsertAnimalTaskSqlCommand(int animalId, string name, string frequency, DateOnly scheduledFor)
    : IInsertSqlCommand<AnimalTaskEntity>
{
    public string SqlText { get; } = """
                                     insert into AnimalTasks (AnimalId, Name, Frequency, ScheduledFor)
                                        values (@AnimalId, @Name, @Frequency, @ScheduledFor)
                                        returning Id, AnimalId, Name, Frequency, CreatedAt, ScheduledFor
                                     """;
    public object? Parameters => new { AnimalId = animalId, Name = name, Frequency = frequency, ScheduledFor = scheduledFor };
}