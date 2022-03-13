using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MotoBest.Data;

public class AppDbContext : IdentityDbContext<User, Role, string>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        ChangeDefaultIdentityColumnNames(builder);
    }

    private static void ChangeDefaultIdentityColumnNames(ModelBuilder builder)
        => builder
            .Entity<User>(entity => entity.ToTable("Users"))
            .Entity<Role>(entity => entity.ToTable("Roles"))
            .Entity<IdentityUserRole<string>>(entity => entity.ToTable("UserRoles"))
            .Entity<IdentityUserClaim<string>>(entity => entity.ToTable("UserClaims"))
            .Entity<IdentityUserLogin<string>>(entity => entity.ToTable("UserLogins"))
            .Entity<IdentityRoleClaim<string>>(entity => entity.ToTable("RoleClaims"))
            .Entity<IdentityUserToken<string>>(entity => entity.ToTable("UserTokens"));
}
