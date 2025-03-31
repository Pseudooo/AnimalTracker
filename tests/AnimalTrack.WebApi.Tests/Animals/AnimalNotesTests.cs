using System.Net;
using System.Net.Http.Json;
using AnimalTrack.ClientModels.Models.Animals;
using AnimalTrack.WebApi.Tests.Fixtures;
using Shouldly;
using Xunit;

namespace AnimalTrack.WebApi.Tests.Animals;

public class AnimalNotesTests(AnimalTrackFixture animalTrackFixture) : IClassFixture<AnimalTrackFixture>
{
    private readonly HttpClient _httpClient = animalTrackFixture.CreateClient();
    
    [Fact]
    public async Task GivenKnownAnimal_WhenCreateNote_ShouldReturn200()
    {
        // Arrange
        var uri = new Uri("Animal/2/notes", UriKind.Relative);
        
        // Act
        var response = await _httpClient.PostAsync(uri, JsonContent.Create("This is a cool note"));
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
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
    public async Task GivenUnknownAnimal_WhenCreateNote_ShouldReturn404()
    {
        // Arrange
        var uri = new Uri("Animal/99/notes", UriKind.Relative);
        
        // Act
        var response = await _httpClient.PostAsync(uri, JsonContent.Create("This is a cool note"));
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
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
    public async Task GivenUnknownAnimal_WhenGetNotes_ShouldReturn404()
    {
        // Arrange
        var uri = new Uri("Animal/99/notes", UriKind.Relative);
        
        // Act
        var response = await _httpClient.GetAsync(uri);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.NotFound);
    }
    
    
    [Fact]
    public async Task GivenKnownNote_WhenDelete_ShouldReturn200()
    {
        // Arrange
        var uri = new Uri("Animal/note/99", UriKind.Relative);
        
        // Act
        var response = await _httpClient.DeleteAsync(uri);
        
        // Assert
        response.StatusCode.ShouldBe(HttpStatusCode.OK);
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
}