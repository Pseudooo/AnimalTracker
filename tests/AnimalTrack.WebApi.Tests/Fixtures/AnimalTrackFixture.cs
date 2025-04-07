using System.Text.Json;
using System.Text.Json.Serialization;
using AnimalTrack.Configuration;
using AnimalTrack.WebApi.Fixtures;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AnimalTrack.WebApi.Tests.Fixtures;

public abstract class AnimalTrackFixture : WebApplicationFactory<Program>, IAsyncLifetime
{
    public DatabaseFixture DatabaseFixture { get; protected set; } = new DatabaseFixtureBuilder()
        .WithSeedScript("seed_animals.sql")
        .Build();

    public abstract Task InitializeAsync();

    public new async Task DisposeAsync()
    {
        await DatabaseFixture.DisposeAsync();
        await base.DisposeAsync();
    }
    
    public JsonSerializerOptions JsonSerializerOptions => new()
    {
        Converters = { new JsonStringEnumConverter() },
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var databaseConfigurationDescriptor = services.SingleOrDefault(
                service => service.ServiceType == typeof(DatabaseConfiguration));
            if (databaseConfigurationDescriptor is not null)
                services.Remove(databaseConfigurationDescriptor);

            services.AddSingleton(DatabaseFixture.GetDatabaseConfiguration());
        });
        
        builder.UseEnvironment("Development");
    }
}