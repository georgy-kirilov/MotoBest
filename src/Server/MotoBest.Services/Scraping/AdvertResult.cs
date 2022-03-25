namespace MotoBest.Services.Scraping;

public class AdvertResult
{
    public string Url { get; init; } = "";

    public DateTime ModifiedOn { get; init; }

    public override bool Equals(object? obj)
    {
        if (obj != null && obj is AdvertResult other)
        {
            return Url == other.Url && ModifiedOn == other.ModifiedOn;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Url, ModifiedOn);
    }
}
