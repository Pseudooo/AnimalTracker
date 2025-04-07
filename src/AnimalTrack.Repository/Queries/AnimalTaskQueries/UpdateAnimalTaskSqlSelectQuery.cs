using AnimalTrack.Repository.Entities;
using AnimalTrack.Repository.Interfaces.Queries;

namespace AnimalTrack.Repository.Queries.AnimalTaskQueries;

public class UpdateAnimalTaskSqlSelectQuery(int taskId, string name, string frequency) : ISqlSelectQuery<AnimalTaskEntity>
{
    public string SqlText { get; } = """
                                     update AnimalTasks
                                        set
                                            Name = @Name,
                                            Frequency = @Frequency
                                        where Id = @TaskId
                                        returning Id, AnimalId, Name, CreatedAt
                                     """;
    public object? Parameters => new { TaskId = taskId, Name = name, Frequency = frequency };
}