using AnimalTrack.Configuration;
using AnimalTrack.WebApi.Fixtures;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AnimalTrack.WebApi.Tests.Fixtures;

public class AnimalTrackFixture : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly DatabaseFixture _databaseFixture = new DatabaseFixtureBuilder()
        .WithSeedScript("seed_animals.sql")
        .Build();

    public async Task InitializeAsync()
    {
        await _databaseFixture.StartAsync();
    }

    public new async Task DisposeAsync()
    {
        await _databaseFixture.DisposeAsync();
        await base.DisposeAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var databaseConfigurationDescriptor = services.SingleOrDefault(
                service => service.ServiceType == typeof(DatabaseConfiguration));
            if (databaseConfigurationDescriptor is not null)
                services.Remove(databaseConfigurationDescriptor);

            services.AddSingleton(_databaseFixture.GetDatabaseConfiguration());
        });
        
        builder.UseEnvironment("Development");
    }
}