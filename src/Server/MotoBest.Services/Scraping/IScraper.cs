using AngleSharp.Dom;

namespace MotoBest.Services.Scraping;

public interface IScraper
{
    AdvertScrapeModel ScrapeAdvert(IDocument document);

    IEnumerable<AdvertResult?> ScrapeAdvertResultsFromPage(IDocument document);
}
