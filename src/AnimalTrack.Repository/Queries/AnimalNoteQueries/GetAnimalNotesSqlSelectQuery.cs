using AnimalTrack.Repository.Entities;
using AnimalTrack.Repository.Interfaces.Queries;

namespace AnimalTrack.Repository.Queries.AnimalNoteQueries;

public class GetAnimalNotesSqlSelectQuery(int animalId) : ISqlSelectQuery<AnimalNoteEntity>
{
    public string SqlText { get; } = """
                                     select
                                            Id,
                                            Note,
                                            CreatedAt
                                        from AnimalNotes
                                            where AnimalId = @AnimalId
                                     """;
    public object? Parameters => new { AnimalId = animalId };
}