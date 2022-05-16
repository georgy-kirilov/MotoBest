using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using MotoBest.Data.Repositories;

using MotoBest.Services.Common;
using MotoBest.Services.Common.Units;

using MotoBest.Services.Data.Features;
using MotoBest.Services.Data.Adverts;
using MotoBest.Services.Data.Adverts.Filtering;

using MotoBest.Services.Mapping;
using MotoBest.Services.Normalization;

using MotoBest.Services.Scraping;
using MotoBest.Services.Scraping.Common;
using MotoBest.Services.Scraping.DesktopAutoBg;

using MotoBest.WebApi;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var identityOptions = builder.Configuration.GetSection("IdentityOptions").Get<IdentityOptions>();

builder.Services
    .AddAutoMapper()
    .AddAppDbContext(connectionString)
    .AddAppIdentity(identityOptions);

builder.Services
    .AddTransient<ISiteScraper, DesktopAutoBgSiteScraper>()
    .AddSingleton<IDateTimeManager, DateTimeManager>()
    .AddScoped(typeof(IRepository<>), typeof(Repository<>))
    .AddTransient(typeof(IFeatureService<>), typeof(FeatureService<>))
    .AddSingleton<ICurrencyCourseProvider, StaticCurrencyCourseProvider>()
    .AddTransient<ISiteDataNormalizer, SiteDataNormalizer>()
    .AddTransient<IAdvertService, AdvertService>()
    .AddTransient<IEuroStandardService, EuroStandardService>()
    .AddTransient<IPopulatedPlaceService, PopulatedPlaceService>()
    .AddTransient<IModelService, ModelService>()
    .AddTransient<IExtraService, ExtraService>()
    .AddTransient<ISearchFilterFactory, SearchFilterOptionsBuilder>()
    .AddTransient<IUnitManager, UnitManager>()
    .AddTransient<AdvertMapper>();

builder.Services.AddHostedService<ScrapingBackgroundService>();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy => policy.WithOrigins("https://localhost:4200", "http://localhost:4200"));
});

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

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
