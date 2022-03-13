using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using MotoBest.Data;
using MotoBest.Data.Models.Identity;

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
}
