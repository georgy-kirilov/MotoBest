namespace MotoBest.Services.Scraping.Models;

public class ScrapedSearchAdvertsResult
{
    public string Url { get; init; } = string.Empty;

    public DateTime ModifiedOn { get; init; }

    public override bool Equals(object? obj)
    {
        if (obj == this)
        {
            return true;
        }

        if (obj == null)
        {
            return false;
        }

        if (obj is not ScrapedSearchAdvertsResult)
        {
            return false;
        }

        var other = obj as ScrapedSearchAdvertsResult;
        return Url == other?.Url && ModifiedOn == other.ModifiedOn;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Url, ModifiedOn);
    }
}
