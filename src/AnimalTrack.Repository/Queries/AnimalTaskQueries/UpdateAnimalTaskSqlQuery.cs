using AnimalTrack.Repository.Entities;
using AnimalTrack.Repository.Interfaces;

namespace AnimalTrack.Repository.Queries.AnimalTaskQueries;

public class UpdateAnimalTaskSqlQuery(int taskId, string name) : ITypedSqlQuery<AnimalTaskEntity>
{
    public string SqlText { get; } = """
                                     update AnimalTasks
                                        set
                                            Name = @Name
                                        where Id = @TaskId
                                        returning Id, AnimalId, Name, CreatedAt
                                     """;
    public object? Parameters => new { TaskId = taskId, Name = name };
}