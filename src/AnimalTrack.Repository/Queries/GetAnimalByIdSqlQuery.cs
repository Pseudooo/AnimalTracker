using AnimalTrack.Repository.Entities;
using AnimalTrack.Repository.Interfaces;

namespace AnimalTrack.Repository.Queries;

public class GetAnimalByIdSqlQuery(int id)
    : ITypedSqlQuery<AnimalEntity>
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