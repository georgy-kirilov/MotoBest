using MotoBest.Services.Scraping.Models;

namespace MotoBest.Services.Normalization;

public interface ISiteDataNormalizer
{
    NormalizedAdvert NormalizeAdvert(ScrapedAdvert scrapedAdvert);
}
