namespace AnimalTrack.Repository.Interfaces.Queries;

public interface ISqlCommand
{
    public string SqlText { get; }
    public object? Parameters { get; }
}