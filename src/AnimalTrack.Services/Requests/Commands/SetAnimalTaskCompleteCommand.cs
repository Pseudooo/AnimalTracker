namespace AnimalTrack.Services.Requests.Commands;

public record SetAnimalTaskCompleteCommand(int AnimalTaskId) : ICommand<bool>;