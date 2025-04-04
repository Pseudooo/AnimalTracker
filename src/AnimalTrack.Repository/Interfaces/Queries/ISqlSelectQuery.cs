namespace AnimalTrack.Repository.Interfaces.Queries;

public interface ISqlSelectQuery<T>
{
    public string SqlText { get; }
    public object? Parameters { get; }
}