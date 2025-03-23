using System.Net;
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
        _databaseFixture = new DatabaseFixture();
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
}