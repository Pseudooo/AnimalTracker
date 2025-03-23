using System.Reflection;

namespace AnimalTrack.WebApi.Tests.Fixtures;

public class DatabaseFixtureBuilder
{
    private static readonly Lazy<string> SeedScriptsRootPath = new(
        () => Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!, "SeedScripts"));
    
    private readonly List<string> _seedScripts = [];

    public DatabaseFixtureBuilder WithSeedScript(string scriptPath)
    {
        var path = Path.Combine(SeedScriptsRootPath.Value, scriptPath);
        var script = File.ReadAllText(path);
        _seedScripts.Add(script);
        return this;
    }

    public DatabaseFixture Build() => new(_seedScripts.ToArray());
}