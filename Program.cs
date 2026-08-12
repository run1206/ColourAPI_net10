var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Do not enforce HTTPS redirection inside the container runtime.
// HTTPS termination is typically handled by a reverse proxy in container deployments.

app.MapControllers();

app.MapGet("/api/values", () =>
{
    return new string[] { "value1", "value2" };
});

app.MapGet("/api/values/{id}", (int id) =>
{
    return "value";
});

app.Run();