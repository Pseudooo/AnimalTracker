using System.Net;
using System.Net.Http.Json;
using AnimalTrack.ClientModels;
using AnimalTrack.WebApi.Tests.Fixtures;
using Shouldly;
using Xunit;

namespace AnimalTrack.WebApi.Tests;

public class AnimalTests : IAsyncLifetime
{
    private DatabaseFixture _databaseFixture = null!;
    private AnimalTrackFixture _animalTrackFixture = null!;
    private HttpClient _httpClient = null!;
    
    public async Task InitializeAsync()
    {
        _databaseFixture = new DatabaseFixtureBuilder()
            .WithSeedScript("seed_animals.sql")
            .Build();
        await _databaseFixture.StartAsync();
        
        _animalTrackFixture = new AnimalTrackFixture(_databaseFixture.GetDatabaseConfiguration());
        _httpClient = _animalTrackFixture.CreateClient();
    }

    public async Task DisposeAsync()
    {
        await _databaseFixture.DisposeAsync();
        await _animalTrackFixture.DisposeAsync();
    }

    [Fact]
    public async Task GivenUnknownAnimalId_WhenGet_ShouldReturn404()
    {
        // Arrange
        var uri = new Uri("Animal/99", UriKind.Relative);
        
        // Act
        var response = await _httpClient.GetAsync(uri);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task GivenKnownAnimalId_WhenGet_ShouldReturn200()
    {
        // Arrange
        var uri = new Uri("Animal/1", UriKind.Relative);
        
        // Act
        var response = await _httpClient.GetAsync(uri);
        var animal = await response.Content.ReadFromJsonAsync<AnimalModel>();
        
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        animal.ShouldNotBeNull();
        animal.Id.ShouldBe(1);
        animal.Name.ShouldBe("Alice");
        animal.CreatedAt.ShouldBeGreaterThan(DateTime.UtcNow.Subtract(TimeSpan.FromHours(1)));
    }
}