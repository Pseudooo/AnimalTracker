using System.Net;
using System.Net.Http.Json;
using AnimalTrack.ClientModels.Models.Animals;
using AnimalTrack.WebApi.Tests.Fixtures;
using Shouldly;
using Xunit;

namespace AnimalTrack.WebApi.Tests.Animals;

public class AnimalTasksTests(AnimalTrackFixture animalTrackFixture) : IClassFixture<AnimalTrackFixture>
{
    private readonly HttpClient _httpClient = animalTrackFixture.CreateClient();

    [Fact]
    public async Task GivenKnownAnimalWithTasks_WhenGetTasks_ShouldReturnTasksWith200()
    {
        // Arrange
        var uri = new Uri("Animal/5/tasks", UriKind.Relative);
        
        // Act
        var response = await _httpClient.GetAsync(uri);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<List<AnimalTaskModel>>();
        
        // Assert
        result.ShouldNotBeNull();
        result.Count.ShouldNotBe(0);
        result.ShouldContain(task => task.Name == "Feed me");
        result.ShouldContain(task => task.Name == "Wash me");
    }
}