using AngleSharp.Dom;
using MotoBest.Services.Scraping.Models;

namespace MotoBest.Services.Scraping;

public interface ISiteScraper
{
    ScrapedAdvert ScrapeAdvert(IDocument document);

    IEnumerable<SearchAdvertResult?> ScrapeSearchAdvertResults(IDocument document);
}
