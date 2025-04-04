using System.Net;
using System.Net.Http.Json;
using AnimalTrack.ClientModels.Models.Animals;
using AnimalTrack.WebApi.Tests.Extensions;
using AnimalTrack.WebApi.Tests.Fixtures;
using Shouldly;
using Xunit;

namespace AnimalTrack.WebApi.Tests.Animals;

public class AnimalNotesTests(AnimalNotesTests.AnimalTrackNotesFixture animalTrackFixture) : IClassFixture<AnimalNotesTests.AnimalTrackNotesFixture>
{
    private readonly HttpClient _httpClient = animalTrackFixture.CreateClient();
    
    [Fact]
    public async Task GivenKnownAnimal_WhenCreateNote_ShouldReturn200()
    {
        // Arrange
        var uri = new Uri("Animal/2/notes", UriKind.Relative);
        const string note = "This is a cool note";
        
        // Act
        var response = await _httpClient.PostAsync(uri, JsonContent.Create(note));
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var createdNote = await response.Content.ReadFromJsonAsync<AnimalNoteModel>();
        
        // Assert
        createdNote.ShouldNotBeNull();
        createdNote.Id.ShouldNotBe(0);
        createdNote.Note.ShouldBe(note);
        createdNote.CreatedAt.ShouldBeGreaterThan(DateTime.UtcNow.Subtract(TimeSpan.FromSeconds(10)));
        await animalTrackFixture.DatabaseFixture.AssertOnDatabaseReader("AnimalNotes", createdNote.Id, reader =>
        {
            reader["Note"].ShouldBe(note);
            reader["CreatedAt"].ShouldBeOfType<DateTime>();
            var createdAt = (DateTime)reader["CreatedAt"];
            createdAt.ShouldBeGreaterThan(DateTime.UtcNow.Subtract(TimeSpan.FromSeconds(10)));
        });
    }
    
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("    ")]
    public async Task GivenKnownAnimal_WhenCreateEmptyNote_ShouldReturn400(string? note)
    {
        // Arrange
        var uri = new Uri("Animal/3/notes", UriKind.Relative);
        
        // Act
        var response = await _httpClient.PostAsync(uri, JsonContent.Create(note));
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task GivenKnownAnimal_WhenGetNotes_ShouldReturn200()
    {
        // Arrange
        var uri = new Uri("Animal/1/notes", UriKind.Relative);
        
        // Act
        var response = await _httpClient.GetAsync(uri);
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
        var notes = await response.Content.ReadFromJsonAsync<List<AnimalNoteModel>>();

        // Assert
        notes.ShouldNotBeNull();
        notes.Count.ShouldBe(2);
        notes.ShouldContain(note => note.Note == "This is a note");
        notes.ShouldContain(note => note.Note == "This is my second note");
    }
    
    [Fact]
    public async Task GivenKnownNote_WhenDelete_ShouldReturn200()
    {
        // Arrange
        var uri = new Uri("Animal/note/3", UriKind.Relative);
        
        // Act
        var response = await _httpClient.DeleteAsync(uri);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NoContent);
        await animalTrackFixture.DatabaseFixture.AssertOnDatabaseReader("AnimalNotes", 3, reader =>
        {
            reader.HasRows.ShouldBeFalse();
        });
    }

    [Fact]
    public async Task GivenUnknownNote_WhenDelete_ShouldReturn404()
    {
        // Arrange
        var uri = new Uri("Animal/note/99", UriKind.Relative);
        
        // Act
        var response = await _httpClient.DeleteAsync(uri);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }

    public class AnimalTrackNotesFixture : AnimalTrackFixture
    {
        public override async Task InitializeAsync()
        {
            DatabaseFixture = new DatabaseFixtureBuilder()
                .WithSeedScript("seed_animals_with_notes.sql")
                .Build();
            await DatabaseFixture.StartAsync();
        }
    }
}