using System.Collections.Immutable;

namespace AnimalTrack.Services.Exceptions;

public class ValidationException(IReadOnlyList<string> failures)
    : Exception(string.Join(", ", failures))
{
    private IReadOnlyList<string> _failures = failures.ToImmutableArray();
}