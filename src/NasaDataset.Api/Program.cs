using Microsoft.EntityFrameworkCore;
using NasaDataset.Api.Middlewares;
using NasaDataset.Application;
using NasaDataset.Application.Meteorites.Interfaces;
using NasaDataset.Application.Meteorites.Services;
using NasaDataset.Infrastructure;
using NasaDataset.Infrastructure.Configuration;
using NasaDataset.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Добавляем CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:53684")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.AddInfrastructureServices();
builder.AddApplicationServices();

builder.Services.AddHttpClient();

builder.Services.AddControllers();

builder.Services.AddMemoryCache();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();

    var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

    await initialiser.InitialiseAsync();
}

app.UseCors("AllowFrontend");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
