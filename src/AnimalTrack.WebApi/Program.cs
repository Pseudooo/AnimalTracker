using AnimalTrack.Configuration;
using AnimalTrack.Repository;
using AnimalTrack.Repository.Interfaces;
using AnimalTrack.Repository.Repositories;
using AnimalTrack.Services.Extensions;
using Asp.Versioning;
using Microsoft.OpenApi.Models;

namespace AnimalTrack.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = new HeaderApiVersionReader();
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "AnimalTrack API v1.0",
                Version = "v1.0",
            });
        });
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
            services.AddTransient<IAnimalRepository, AnimalRepository>();
        }
    }
}