using AngleSharp;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using MotoBest.Data.Repositories;

using MotoBest.Services.Common;
using MotoBest.Services.Data;
using MotoBest.Services.Normalization;
using MotoBest.Services.Scraping;
using MotoBest.Services.Scraping.Common;
using MotoBest.Services.Scraping.DesktopAutoBg;

using MotoBest.WebApi;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var identityOptions = builder.Configuration.GetSection("IdentityOptions").Get<IdentityOptions>();

builder.Services
    .AddAppDbContext(connectionString)
    .AddAppIdentity(identityOptions);

builder.Services
    .AddTransient<ISiteScraper, DesktopAutoBgSiteScraper>()
    .AddSingleton<IDateTimeManager, DateTimeManager>()
    .AddScoped(typeof(IRepository<>), typeof(Repository<>))
    .AddSingleton<ICurrencyCourseProvider, StaticCurrencyCourseProvider>()
    .AddTransient<ISiteDataNormalizer, SiteDataNormalizer>()
    .AddTransient<IAdvertService, AdvertService>()
    .AddTransient(serviceProvider => BrowsingContext.New(Configuration.Default.WithDefaultLoader()));

builder.Services.AddHostedService<ScrapingBackgroundService>();

builder.Services.AddControllers();

builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

var app = builder.Build();

app.ApplyMigrations().SeedData();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
