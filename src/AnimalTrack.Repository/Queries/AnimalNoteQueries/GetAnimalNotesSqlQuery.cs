using AnimalTrack.Repository.Entities;
using AnimalTrack.Repository.Interfaces;

namespace AnimalTrack.Repository.Queries.AnimalNoteQueries;

public class GetAnimalNotesSqlQuery(int animalId) : ITypedSqlQuery<AnimalNoteEntity>
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