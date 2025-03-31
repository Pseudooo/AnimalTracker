namespace AnimalTrack.Services.Requests.Commands;

public record DeleteAnimalNoteCommand(int NoteId) : ICommand<bool>;