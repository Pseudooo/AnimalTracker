namespace AnimalTrack.Services.Requests.Commands;

public class SetAnimalTaskCompleteCommand(int AnimalTaskId) : ICommand<bool>;