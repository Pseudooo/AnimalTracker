using AnimalTrack.Repository.Entities;
using AnimalTrack.Repository.Interfaces.Queries;

namespace AnimalTrack.Repository.Queries;

public class GetAnimalPageSqlSelectQuery(int pageSize, int pageNumber)
    : ISqlSelectQuery<AnimalEntity>
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