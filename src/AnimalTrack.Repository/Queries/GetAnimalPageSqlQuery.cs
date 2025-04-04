using AnimalTrack.Repository.Entities;
using AnimalTrack.Repository.Interfaces;

namespace AnimalTrack.Repository.Queries;

public class GetAnimalPageSqlQuery(int pageSize, int pageNumber)
    : ITypedSqlQuery<AnimalEntity>
{
    public string SqlText { get; } = """
                                     select
                                            Id,
                                            Name,
                                            CreatedAt
                                        from Animals
                                            order by Id
                                            limit @Take
                                            offset @Skip
                                     """;
    public object? Parameters => new { Take = pageSize, Skip = (pageNumber - 1) * pageSize };
}