using MotoBest.Services.Scraping.Models;

namespace MotoBest.Services.Normalizing;

public interface INormalizer
{
    NormalizedAdvert Normalize(ScrapedAdvert scrapedAdvert);
}
