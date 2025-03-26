using AnimalTrack.Repository.Entities;
using AnimalTrack.Repository.Interfaces;
using AnimalTrack.Repository.Providers;
using AnimalTrack.Repository.Repositories;
using AnimalTrack.WebApi.Fixtures;
using AnimalTrack.WebApi.Tests.Fixtures;
using BenchmarkDotNet.Attributes;

namespace AnimalTrack.Repository.Benchmarks;

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
        var postgreSqlQueryProvider = new PostgreSqlQueryProvider();
        _animalRepository = new AnimalRepository(postgreSqlQueryProvider, postgreSqlClient);
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