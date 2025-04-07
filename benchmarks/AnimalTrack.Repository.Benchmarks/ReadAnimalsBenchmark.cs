using AnimalTrack.Repository.Entities;
using AnimalTrack.Repository.Interfaces;
using AnimalTrack.Repository.Repositories;
using AnimalTrack.WebApi.Fixtures;
using AnimalTrack.WebApi.Tests.Fixtures;
using BenchmarkDotNet.Attributes;

namespace AnimalTrack.Repository.Benchmarks;

[JsonExporter]
[MemoryDiagnoser]
public class ReadAnimalsBenchmark
{
    private DatabaseFixture _databaseFixture = null!;
    private IAnimalRepository _animalRepository = null!;
    
    [GlobalSetup]
    public async Task SetUp()
    {
        _databaseFixture = new DatabaseFixtureBuilder()
            .WithSeedScript("seed_1k_animals.sql")
            .Build();
        await _databaseFixture.StartAsync();
        
        var databaseConfiguration = _databaseFixture.GetDatabaseConfiguration();
        var postgreSqlConnectionFactory = new PostgreSqlConnectionFactory(databaseConfiguration);
        var postgreSqlClient = new PostgreSqlClient(postgreSqlConnectionFactory);
        _animalRepository = new AnimalRepository(postgreSqlClient);
    }

    [GlobalCleanup]
    public async Task Cleanup()
    {
        await _databaseFixture.DisposeAsync();
    }

    [Benchmark]
    public async Task<List<AnimalEntity>> GetAnimals()
    {
        return await _animalRepository.GetAnimalPage(1, 1000);
    }
}