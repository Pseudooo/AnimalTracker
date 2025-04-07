using AnimalTrack.Repository.Interfaces.Queries;

namespace AnimalTrack.Repository.Queries.AnimalNoteQueries;

public class DeleteAnimalNoteSqlCommand(int noteId) : ISqlDeleteCommand
{
    public string SqlText { get; } = """
                                     delete from AnimalNotes
                                        where Id = @NoteId;
                                     """;
    public object? Parameters => new { NoteId = noteId };
}