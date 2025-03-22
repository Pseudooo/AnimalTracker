namespace AnimalTrack.Repository.Configuration;

public class DatabaseConfiguration
{
    public required string Host { get; init; }
    public required int Port { get; init; }
    public required string Database { get; init; }
    public required string User { get; init; }
    public required string Password { get; init; }
}