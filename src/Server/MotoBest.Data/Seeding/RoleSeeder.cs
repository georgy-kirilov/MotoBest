using Microsoft.Extensions.DependencyInjection;

using MotoBest.Data.Models.Identity;

namespace MotoBest.Data.Seeding;

internal class RoleSeeder : ISeeder
{
    public async Task SeedAsync(AppDbContext dbContext, IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
        await SeedRoleAsync(roleManager, GlobalConstants.AdminRoleName);
    }

    private static async Task SeedRoleAsync(RoleManager<Role> roleManager, string roleName)
    {
        Role role = await roleManager.FindByNameAsync(roleName);

        if (role == null)
        {
            var result = await roleManager.CreateAsync(new Role(roleName));

            if (!result.Succeeded)
            {
                string message = string.Join(
                    Environment.NewLine, result.Errors.Select(e => e.Description));

                throw new Exception(message);
            }
        }
    }
}
