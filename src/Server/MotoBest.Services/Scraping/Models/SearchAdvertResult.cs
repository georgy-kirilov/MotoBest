namespace MotoBest.Services.Scraping.Models;

public class SearchAdvertResult
{
    public string Url { get; init; } = "";

    public DateTime ModifiedOn { get; init; }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj is not SearchAdvertResult)
        {
            return false;
        }

        var other = obj as SearchAdvertResult;
        return Url == other?.Url && ModifiedOn == other.ModifiedOn;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Url, ModifiedOn);
    }
}
