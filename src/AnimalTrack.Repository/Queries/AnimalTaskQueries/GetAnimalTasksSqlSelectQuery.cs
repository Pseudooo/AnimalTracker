using AnimalTrack.Repository.Entities;
using AnimalTrack.Repository.Interfaces.Queries;

namespace AnimalTrack.Repository.Queries.AnimalTaskQueries;

public class GetAnimalTasksSqlSelectQuery(int animalId) : ISqlSelectQuery<AnimalTaskEntity>
{
    public string SqlText { get; } = """
                                        select
                                                Id,
                                                AnimalId,
                                                Name,
                                                CreatedAt
                                            from AnimalTasks
                                                where AnimalId = @AnimalId
                                     """;
    public object? Parameters => new { AnimalId = animalId };
}