using AnimalTrack.Repository;
using AnimalTrack.Repository.Configuration;
using AnimalTrack.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

RegisterRepositoryDependencies(builder.Services, builder.Configuration);

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
}
