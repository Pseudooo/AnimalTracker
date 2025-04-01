using System.Reflection;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;

Assembly[] benchmarkAssemblies =
[
    typeof(AnimalTrack.Repository.Benchmarks.Program).Assembly,
];

var config = ManualConfig.Create(DefaultConfig.Instance)
    .WithOption(ConfigOptions.JoinSummary, true);
    
BenchmarkSwitcher.FromAssemblies(benchmarkAssemblies).Run(args, config);
