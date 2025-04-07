using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using AnimalTrack.ClientModels.Constants;
using AnimalTrack.ClientModels.Models.Animals;
using AnimalTrack.WebApi.Requests;
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
        var body = new AnimalTaskRequestBody()
        {
            Name = "My new task",
            Frequency = SchedulingFrequency.OneOff,
            ScheduledFor = new DateOnly(2025, 8, 27),
        };
        var jsonOptions = new JsonSerializerOptions()
        {
            Converters = { new JsonStringEnumConverter() },
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        
        // Act
        var response = await _httpClient.PostAsync(uri, JsonContent.Create(body, options: jsonOptions));
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var createdTask = await response.Content.ReadFromJsonAsync<AnimalTaskModel>(jsonOptions);
        
        // Assert
        createdTask.ShouldNotBeNull();
        createdTask.Id.ShouldNotBe(0);
        createdTask.Name.ShouldBe(body.Name);
        createdTask.Frequency.ShouldBe(body.Frequency);
        createdTask.CreatedAt.ShouldBeGreaterThan(DateTime.UtcNow.Subtract(TimeSpan.FromSeconds(10)));
        createdTask.ScheduledFor.ShouldBe(body.ScheduledFor);
        await animalTrackFixture.DatabaseFixture.AssertOnDatabaseReader("AnimalTasks", createdTask.Id, reader =>
        {
            reader.HasRows.ShouldBeTrue();
            reader["Name"].ShouldBe(body.Name);
            reader["Frequency"].ShouldBe(body.Frequency.ToString());
            reader["CreatedAt"].ShouldBeOfType<DateTime>();
            var createdAt = (DateTime)reader["CreatedAt"];
            createdAt.ShouldBeGreaterThan(DateTime.UtcNow.Subtract(TimeSpan.FromSeconds(10)));
            var scheduledFor = DateOnly.FromDateTime((DateTime) reader["ScheduledFor"]);
            scheduledFor.ShouldBe(body.ScheduledFor);
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
        var result = await response.Content.ReadFromJsonAsync<List<AnimalTaskModel>>(animalTrackFixture.JsonSerializerOptions);
        
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
        var body = new AnimalTaskRequestBody()
        {
            Name = "My updated task",
            Frequency = SchedulingFrequency.OneOff,
            ScheduledFor = new DateOnly(2025, 8, 27),
        };
        
        // Act
        var response = await _httpClient.PutAsync(uri, JsonContent.Create(body, options: animalTrackFixture.JsonSerializerOptions));
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        await animalTrackFixture.DatabaseFixture.AssertOnDatabaseReader("AnimalTasks", 3, reader =>
        {
            reader["Name"].ShouldBe(body.Name);
            reader["Frequency"].ShouldBe(body.Frequency.ToString());
            var scheduledFor = DateOnly.FromDateTime((DateTime) reader["ScheduledFor"]);
            scheduledFor.ShouldBe(body.ScheduledFor);
        });
    }

    [Fact]
    public async Task GivenUnknownTask_WhenUpdate_ShouldReturn404()
    {
        // Arrange
        var uri = new Uri("Animal/tasks/99", UriKind.Relative);
        var body = new AnimalTaskRequestBody()
        {
            Name = "my updated task",
            Frequency = SchedulingFrequency.OneOff
        };

        // Act
        var response = await _httpClient.PutAsync(uri, JsonContent.Create(body, options: animalTrackFixture.JsonSerializerOptions));
        
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