using AnimalTrack.Repository.Entities;
using AnimalTrack.Repository.Interfaces;

namespace AnimalTrack.Repository.Queries.AnimalNoteQueries;

public class InsertAnimalNoteSqlQuery(int animalId, string note) : ITypedSqlQuery<AnimalNoteEntity>
{
    public string SqlText { get; } = """
                                     insert into AnimalNotes (AnimalId, Note)
                                        values (@AnimalId, @Note)
                                        returning Id, AnimalId, Note, CreatedAt;
                                     """;
    public object? Parameters => new { AnimalId = animalId, Note = note };
}