using System.Collections.Immutable;

namespace AnimalTrack.Services.Exceptions;

public class RequestValidationException(IReadOnlyList<string> failures)
    : Exception(string.Join(", ", failures))
{
    public IReadOnlyList<string> Failures { get; } = failures.ToImmutableArray();
}