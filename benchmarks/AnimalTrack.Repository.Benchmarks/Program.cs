using BenchmarkDotNet.Running;

namespace AnimalTrack.Repository.Benchmarks;

public static class Program
{
    public static void Main()
    {
        BenchmarkRunner.Run<ReadAnimalsBenchmark>();
    }
}