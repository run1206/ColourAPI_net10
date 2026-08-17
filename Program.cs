using ColourAPI_net10.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddControllers();
builder.Services.AddOpenApi();

var configuration = builder.Configuration;
var server = configuration["DBServer"] ?? "ms-sql-server";
var port = configuration["DBPort"] ?? "1433";
var user = configuration["DBUser"] ?? "SA";
var password = configuration["DBPassword"] ?? "Pa55w0rd2026";
var database = configuration["Database"] ?? "Colours";

builder.Services.AddDbContext<ColourContext>(opt => 
    opt.UseSqlServer($"Server={server},{port};Initial Catalog={database};User ID={user};Password={password};Encrypt=False;TrustServerCertificate=True;"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Do not enforce HTTPS redirection inside the container runtime.
// HTTPS termination is typically handled by a reverse proxy in container deployments.

app.MapControllers();

app.MapGet("/api/values", (ColourContext context) =>
{
    return Results.Ok(context.ColourItems.ToList());
});

PrepDB.PrepColours(app);
app.Run();