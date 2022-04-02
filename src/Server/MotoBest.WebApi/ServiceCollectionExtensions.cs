﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using MotoBest.Data;
using MotoBest.Data.Models.Identity;
using MotoBest.Data.Seeding;

namespace MotoBest.WebApi;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAppDbContext(
        this IServiceCollection serviceCollection, string connectionString)
        => serviceCollection.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

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
