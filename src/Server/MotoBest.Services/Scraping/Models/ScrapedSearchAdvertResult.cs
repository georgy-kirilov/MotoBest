namespace MotoBest.Services.Scraping.Models;

public class ScrapedSearchAdvertResult
{
    public string Url { get; init; } = string.Empty;

    public DateTime ModifiedOn { get; init; }

    public override bool Equals(object? obj)
    {
        if (obj == null || obj is not ScrapedSearchAdvertResult)
        {
            return false;
        }

        var other = obj as ScrapedSearchAdvertResult;
        return Url == other?.Url && ModifiedOn == other.ModifiedOn;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Url, ModifiedOn);
    }
}
