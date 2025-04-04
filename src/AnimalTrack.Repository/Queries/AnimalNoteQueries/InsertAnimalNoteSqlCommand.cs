using AnimalTrack.Repository.Entities;
using AnimalTrack.Repository.Interfaces.Queries;

namespace AnimalTrack.Repository.Queries.AnimalNoteQueries;

public class InsertAnimalNoteSqlCommand(int animalId, string note) : IInsertSqlCommand<AnimalNoteEntity>
{
    public string SqlText { get; } = """
                                     insert into AnimalNotes (AnimalId, Note)
                                        values (@AnimalId, @Note)
                                        returning Id, AnimalId, Note, CreatedAt;
                                     """;
    public object? Parameters => new { AnimalId = animalId, Note = note };
}