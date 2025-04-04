namespace AnimalTrack.Repository.Interfaces;

public interface ITypedSqlQuery<T>
{
    public string SqlText { get; }
    public object? Parameters { get; }
}