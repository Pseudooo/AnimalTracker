using AnimalTrack.Configuration;
using AnimalTrack.Repository;
using AnimalTrack.Repository.Interfaces;
using AnimalTrack.Repository.Providers;
using AnimalTrack.Repository.Repositories;
using AnimalTrack.Services.Extensions;

namespace AnimalTrack.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();

        RegisterRepositoryDependencies(builder.Services, builder.Configuration);
        builder.Services.RegisterServiceDependencies();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.MapControllers();
        app.Run();
        return;

        void RegisterRepositoryDependencies(IServiceCollection services, IConfiguration configuration)
        {
            var databaseConfiguration = configuration.GetSection("Repository:Database")
                .Get<DatabaseConfiguration>()!;
            services.AddSingleton(databaseConfiguration);
            services.AddTransient<IPostgreSqlConnectionFactory, PostgreSqlConnectionFactory>();
            services.AddTransient<IPostgreSqlClient, PostgreSqlClient>();
            services.AddTransient<IPostgreSqlQueryProvider, PostgreSqlQueryProvider>();
            services.AddTransient<IAnimalRepository, AnimalRepository>();
        }
    }
}