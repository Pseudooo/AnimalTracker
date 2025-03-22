using AnimalTrack.Repository;
using AnimalTrack.Repository.Configuration;
using AnimalTrack.Repository.Interfaces;
using AnimalTrack.Repository.Providers;
using AnimalTrack.Repository.Repositories;
using AnimalTrack.Services.Extensions;

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
    var repositoryConfiguration = configuration.GetSection("Repository:Database");
    services.Configure<DatabaseConfiguration>(repositoryConfiguration);
    services.AddTransient<IPostgreSqlConnectionFactory, PostgreSqlConnectionFactory>();
    services.AddTransient<IPostgreSqlClient, PostgreSqlClient>();
    services.AddTransient<IPostgreSqlQueryProvider, PostgreSqlQueryProvider>();
    services.AddTransient<IAnimalRepository, AnimalRepository>();
}
