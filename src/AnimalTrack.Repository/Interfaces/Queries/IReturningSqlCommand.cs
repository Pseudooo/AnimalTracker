namespace AnimalTrack.Repository.Interfaces.Queries;

public interface IReturningSqlCommand<T>
{
    public string SqlText { get; }
    public object? Parameters { get; }
}