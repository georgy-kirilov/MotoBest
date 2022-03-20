using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using MotoBest.Services.Scraping;
using MotoBest.WebApi;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var identityOptions = builder.Configuration.GetSection("IdentityOptions").Get<IdentityOptions>();

builder.Services
    .AddAppDbContext(connectionString)
    .AddAppIdentity(identityOptions);

builder.Services.AddTransient<IScraper, AutoBgScraper>();
builder.Services.AddHostedService<ScrapingBackgroundService>();

builder.Services.AddControllers();

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

var app = builder.Build();

app.ApplyMigrations();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
