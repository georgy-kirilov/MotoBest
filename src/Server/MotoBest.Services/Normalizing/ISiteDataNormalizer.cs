using MotoBest.Services.Scraping.Models;

namespace MotoBest.Services.Normalizing;

public interface ISiteDataNormalizer
{
    NormalizedAdvert NormalizeAdvert(ScrapedAdvert scrapedAdvert);
}
