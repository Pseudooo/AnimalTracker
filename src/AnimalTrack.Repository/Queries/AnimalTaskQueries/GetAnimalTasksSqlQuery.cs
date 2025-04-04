using AnimalTrack.Repository.Entities;
using AnimalTrack.Repository.Interfaces;

namespace AnimalTrack.Repository.Queries.AnimalTaskQueries;

public class GetAnimalTasksSqlQuery(int animalId) : ITypedSqlQuery<AnimalTaskEntity>
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