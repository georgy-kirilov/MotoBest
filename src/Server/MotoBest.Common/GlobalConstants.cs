using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace MotoBest.Common;

public static class GlobalConstants
{
    public const string AdminRoleName = "Admin";

    public const string Whitespace = " ";

    public const string BulgarianCultureInfo = "bg-BG";

    public static readonly JsonSerializerOptions BasicJsonOptions = new()
    {
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
        WriteIndented = true
    };

    public const string SeedingResourcesPath = "../../Server/MotoBest.Data/Seeding/Resources";
}
