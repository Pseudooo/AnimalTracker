using AnimalTrack.Repository.Interfaces.Queries;

namespace AnimalTrack.Repository.Queries.AnimalTaskQueries;

public class SetAnimalTaskToCompleteQuery(int taskId) : ISqlSelectQuery<int?>
{
    public string SqlText { get; } = """
                                     update AnimalTasks
                                        set ScheduledFor = NOW()
                                        where Id = @TaskId
                                        returning Id
                                     """;
    public object? Parameters => new { TaskId = taskId };
}
