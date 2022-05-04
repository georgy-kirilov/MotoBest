using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.EntityFrameworkCore;

using MotoBest.Data.Models.Identity;

namespace MotoBest.Data;

public class AppDbContext : IdentityDbContext<User, Role, string>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Advert> Adverts { get; init; } = default!;

    public DbSet<BodyStyle> BodyStyles { get; init; } = default!;

    public DbSet<Brand> Brands { get; init; } = default!;

    public DbSet<Color> Colors { get; init; } = default!;

    public DbSet<Condition> Conditions { get; init; } = default!;

    public DbSet<Engine> Engines { get; init; } = default!;

    public DbSet<EuroStandard> EuroStandards { get; init; } = default!;

    public DbSet<Image> Images { get; init; } = default!;

    public DbSet<Model> Models { get; init; } = default!;

    public DbSet<Region> Regions { get; init; } = default!;

    public DbSet<Site> Sites { get; init; } = default!;

    public DbSet<PopulatedPlace> PopulatedPlaces { get; init; } = default!;

    public DbSet<Transmission> Transmissions { get; init; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        ChangeDefaultIdentityColumnNames(builder);

        builder
            .Entity<Advert>()
            .HasIndex(adv => adv.RemoteId)
            .IsUnique();

        builder.AddUniqueConstraintTo(
            nameof(Feature.Name),
            typeof(BodyStyle),
            typeof(Brand),
            typeof(Color),
            typeof(Condition),
            typeof(Engine),
            typeof(EuroStandard),
            typeof(Region),
            typeof(Site),
            typeof(Transmission));

        builder.Entity<Advert>()
            .HasMany(adv => adv.Images)
            .WithOne(img => img.Advert)
            .HasForeignKey(img => img.AdvertId);
    }

    private static void ChangeDefaultIdentityColumnNames(ModelBuilder builder)
        => builder
            .Entity<User>(e => e.ToTable("Users"))
            .Entity<Role>(e => e.ToTable("Roles"))
            .Entity<IdentityUserRole<string>>(e => e.ToTable("UserRoles"))
            .Entity<IdentityUserClaim<string>>(e => e.ToTable("UserClaims"))
            .Entity<IdentityUserLogin<string>>(e => e.ToTable("UserLogins"))
            .Entity<IdentityRoleClaim<string>>(e => e.ToTable("RoleClaims"))
            .Entity<IdentityUserToken<string>>(e => e.ToTable("UserTokens"));
}
