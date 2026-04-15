var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

// Hello World endpoint
app.MapGet("/", () => "Hello World from .NET 10")
    .WithName("HelloWorld");

app.Run();
