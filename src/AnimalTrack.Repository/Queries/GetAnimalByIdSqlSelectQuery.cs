using AnimalTrack.Repository.Entities;
using AnimalTrack.Repository.Interfaces.Queries;

namespace AnimalTrack.Repository.Queries;

public class GetAnimalByIdSqlSelectQuery(int id)
    : ISqlSelectQuery<AnimalEntity>
{
    public string SqlText { get; } = """
                                     select
                                            Id,
                                            Name,
                                            CreatedAt
                                        from Animals
                                        where Id = @Id
                                     """;

    public object? Parameters => new { Id = id };
}