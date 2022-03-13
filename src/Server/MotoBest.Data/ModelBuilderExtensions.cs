using Microsoft.EntityFrameworkCore;

namespace MotoBest.Data;

public static class ModelBuilderExtensions
{
    public static void AddUniqueConstraintTo(
        this ModelBuilder builder,
        string uniquePropertyName,
        params Type[] types)
    {
        foreach (var type in types)
        {
            builder.Entity(type).HasIndex(uniquePropertyName).IsUnique();
        }
    }
}
