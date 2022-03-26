using AngleSharp.Dom;

namespace MotoBest.Services.Scraping;

public interface IScraper
{
    ScrapedAdvert ScrapeAdvert(IDocument document);

    IEnumerable<AdvertResult?> ScrapeAdvertResultsFromPage(IDocument document);
}
