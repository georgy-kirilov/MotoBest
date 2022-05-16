using AutoMapper;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using MotoBest.Data;
using MotoBest.Data.Models.Identity;
using MotoBest.Data.Seeding;

using MotoBest.Services.Common.Units;
using MotoBest.Services.Mapping;

namespace MotoBest.WebApi;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppDbContext(
        this IServiceCollection serviceCollection, string connectionString)
        => serviceCollection
            .AddDbContext<AppDbContext>(
                options => options.UseLazyLoadingProxies().UseSqlServer(connectionString),
                ServiceLifetime.Transient);

    public static IServiceCollection AddAutoMapper(this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddSingleton(provider => new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(new Profile[]
                {
                    new FeatureProfile(),
                    new AdvertProfile(provider.GetRequiredService<IUnitManager>())
                });
            })
            .CreateMapper());

        return serviceCollection;
    }

    public static IdentityBuilder AddAppIdentity(
        this IServiceCollection serviceCollection, IdentityOptions identityOptions)
        => serviceCollection
            .AddIdentity<User, Role>(options =>
            {
                options.Password = identityOptions.Password;
                options.SignIn = identityOptions.SignIn;
            })
            .AddEntityFrameworkStores<AppDbContext>();

    public static WebApplication ApplyMigrations(this WebApplication app)
    {
        using var serviceScope = app.Services.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate();
        return app;
    }

    public static WebApplication SeedData(this WebApplication app)
    {
        using var serviceScope = app.Services.CreateScope();
        var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
        new AppSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
        return app;
    }
}
