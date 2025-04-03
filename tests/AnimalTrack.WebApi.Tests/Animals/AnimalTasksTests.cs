using System.Net;
using System.Net.Http.Json;
using AnimalTrack.ClientModels.Models.Animals;
using AnimalTrack.WebApi.Tests.Fixtures;
using Shouldly;
using Xunit;

namespace AnimalTrack.WebApi.Tests.Animals;

public class AnimalTasksTests(AnimalTasksTests.AnimalTrackTasksFixture animalTrackFixture) : IClassFixture<AnimalTasksTests.AnimalTrackTasksFixture>
{
    private readonly HttpClient _httpClient = animalTrackFixture.CreateClient();

    [Fact]
    public async Task GivenKnownAnimalWithTasks_WhenGetTasks_ShouldReturnTasksWith200()
    {
        // Arrange
        var uri = new Uri("Animal/1/tasks", UriKind.Relative);
        
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

    [Fact]
    public async Task GivenKnownAnimalWithoutTasks_WhenGetTasks_ShouldReturnNoTasksWith200()
    {
        // Arrange
        var uri = new Uri("Animal/2/tasks", UriKind.Relative);
        
        // Act
        var response = await _httpClient.GetAsync(uri);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var result = await response.Content.ReadFromJsonAsync<List<AnimalTaskModel>>();
        
        // Assert
        result.ShouldNotBeNull();
        result.Count.ShouldBe(0);
    }

    public class AnimalTrackTasksFixture : AnimalTrackFixture
    {
        public override async Task InitializeAsync()
        {
            DatabaseFixture = new DatabaseFixtureBuilder()
                .WithSeedScript("seed_animals_with_tasks.sql")
                .Build();
            await DatabaseFixture.StartAsync();
        }
    }
}