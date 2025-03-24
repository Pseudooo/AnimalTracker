using BenchmarkDotNet.Running;

namespace AnimalTrack.Repository.Benchmarks;

public class Program
{
    public static void Main()
    {
        BenchmarkRunner.Run<ReadAnimalsBenchmark>();
    }
}