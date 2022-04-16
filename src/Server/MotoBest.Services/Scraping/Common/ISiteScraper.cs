using AngleSharp.Dom;

using MotoBest.Services.Scraping.Models;

namespace MotoBest.Services.Scraping.Common;

public interface ISiteScraper
{
    ScrapedAdvert ScrapeAdvert(IDocument document);

    IEnumerable<ScrapedSearchAdvertResult?> ScrapeSearchAdvertResults(IDocument document);
}
