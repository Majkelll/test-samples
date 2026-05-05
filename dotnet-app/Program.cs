var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Hello World from .NET 10")
    .WithName("HelloWorld");

app.MapGet("/hc", (IConfiguration configuration) =>
{
    // Demo/testing only: exposing runtime configuration like this is not production-ready and must not be used in prod code.
    var settings = BuildSettingsTree(configuration.GetSection("Settings"));

    return Results.Json(new { settings });
});

app.Run();

static object BuildSettingsTree(IConfigurationSection section)
{
    var children = section.GetChildren().ToList();

    if (children.Count == 0)
    {
        return section.Value ?? string.Empty;
    }

    return children.ToDictionary(child => child.Key, BuildSettingsTree);
}
