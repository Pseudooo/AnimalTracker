using System.Net;
using System.Net.Http.Json;
using AnimalTrack.ClientModels;
using AnimalTrack.WebApi.Tests.Fixtures;
using Shouldly;
using Xunit;

namespace AnimalTrack.WebApi.Tests;

public class AnimalTests(AnimalTrackFixture animalTrackFixture) : IClassFixture<AnimalTrackFixture>
{
    private readonly HttpClient _httpClient = animalTrackFixture.CreateClient();
    
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

    [Theory]
    [InlineData(1, "Alice")]
    [InlineData(2, "Bob")]
    public async Task GivenKnownAnimalId_WhenGet_ShouldReturn200(int id, string expectedName)
    {
        // Arrange
        var uri = new Uri($"Animal/{id}", UriKind.Relative);
        
        // Act
        var response = await _httpClient.GetAsync(uri);
        var animal = await response.Content.ReadFromJsonAsync<AnimalModel>();
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        animal.ShouldNotBeNull();
        animal.Id.ShouldBe(id);
        animal.Name.ShouldBe(expectedName);
        animal.CreatedAt.ShouldBeGreaterThan(DateTime.UtcNow.Subtract(TimeSpan.FromHours(1)));
    }

    [Fact]
    public async Task GivenAllAnimalsKnown_WhenGetPage_ShouldReturn200()
    {
        // Arrange
        var uri = new Uri("Animal", UriKind.Relative);
        
        // Act
        var response = await _httpClient.GetAsync(uri);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        
        var animals = await response.Content.ReadFromJsonAsync<List<AnimalModel>>();
        
        // Assert
        animals.ShouldNotBeNull();
        animals.Count.ShouldBe(2);
        
        animals.ShouldContain(x => x.Id == 1);
        var alice = animals.Single(x => x.Id == 1);
        alice.Name.ShouldBe("Alice");
        
        animals.ShouldContain(x => x.Id == 1);
        var bob = animals.Single(x => x.Id == 1);
        bob.Name.ShouldBe("Alice");
    }
}