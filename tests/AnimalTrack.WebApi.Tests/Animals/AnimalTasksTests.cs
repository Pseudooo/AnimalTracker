using System.Net;
using System.Net.Http.Json;
using AnimalTrack.ClientModels.Models.Animals;
using AnimalTrack.WebApi.Tests.Extensions;
using AnimalTrack.WebApi.Tests.Fixtures;
using Shouldly;
using Xunit;

namespace AnimalTrack.WebApi.Tests.Animals;

public class AnimalTasksTests(AnimalTasksTests.AnimalTrackTasksFixture animalTrackFixture) 
    : IClassFixture<AnimalTasksTests.AnimalTrackTasksFixture>
{
    private readonly HttpClient _httpClient = animalTrackFixture.CreateClient();

    [Fact]
    public async Task GivenKnownAnimalAndNewTask_WhenCreateTask_ShouldCreateAndReturnWith200()
    {
        // Arrange
        var uri = new Uri("Animal/3/tasks", UriKind.Relative);
        const string name = "My new task";
        
        // Act
        var response = await _httpClient.PostAsync(uri, JsonContent.Create(name));
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var createdTask = await response.Content.ReadFromJsonAsync<AnimalTaskModel>();
        
        // Assert
        createdTask.ShouldNotBeNull();
        createdTask.Id.ShouldNotBe(0);
        createdTask.Name.ShouldBe(name);
        createdTask.CreatedAt.ShouldBeGreaterThan(DateTime.UtcNow.Subtract(TimeSpan.FromSeconds(10)));
        await animalTrackFixture.DatabaseFixture.AssertOnDatabaseReader("AnimalTasks", createdTask.Id, reader =>
        {
            reader.HasRows.ShouldBeTrue();
            reader["Name"].ShouldBe(name);
            reader["CreatedAt"].ShouldBeOfType<DateTime>();
            var createdAt = (DateTime)reader["CreatedAt"];
            createdAt.ShouldBeGreaterThan(DateTime.UtcNow.Subtract(TimeSpan.FromSeconds(10)));
        });
    }
    
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

    [Fact]
    public async Task GivenKnownTask_WhenUpdate_ShouldReturn204()
    {
        // Arrange
        var uri = new Uri("Animal/tasks/3", UriKind.Relative);
        const string newName = "My updated task";
        
        // Act
        var response = await _httpClient.PutAsync(uri, JsonContent.Create(newName));
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        await animalTrackFixture.DatabaseFixture.AssertOnDatabaseReader("AnimalTasks", 3, reader =>
        {
            reader["Name"].ShouldBe(newName);
        });
    }

    [Fact]
    public async Task GivenUnknownTask_WhenUpdate_ShouldReturn404()
    {
        // Arrange
        var uri = new Uri("Animal/tasks/99", UriKind.Relative);

        // Act
        var response = await _httpClient.PutAsync(uri, JsonContent.Create("my updated task"));
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
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