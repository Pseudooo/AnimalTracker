using AnimalTrack.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalTrack.WebApi.Tests.Fixtures;

public class AnimalTrackFixture(DatabaseConfiguration databaseConfiguration)
    : WebApplicationFactory<Program>
{
    private readonly DatabaseConfiguration _databaseConfiguration = databaseConfiguration;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            var databaseConfigurationDescriptor = services.SingleOrDefault(
                service => service.ServiceType == typeof(DatabaseConfiguration));
            if (databaseConfigurationDescriptor is not null)
                services.Remove(databaseConfigurationDescriptor);

            services.AddSingleton(_databaseConfiguration);
        });
        
        builder.UseEnvironment("Development");
    }
}