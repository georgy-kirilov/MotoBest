using System.Reflection;

namespace MotoBest.Common;

public static class TypeExtensions
{
    public static IEnumerable<T> GetAllPublicConstantValues<T>(this Type type)
    {
        return type
            .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
            .Where(fi => fi != null && fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(T))
            .Select(x => (T)x.GetRawConstantValue()!);
    }
}
