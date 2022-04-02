using MotoBest.Services.Scraping;

namespace MotoBest.Services.Normalizing;

public interface INormalizer
{
    NormalizedAdvert Normalize(ScrapedAdvert scrapedAdvert);
}
