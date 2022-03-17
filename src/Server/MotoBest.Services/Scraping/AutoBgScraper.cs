using AngleSharp.Dom;

namespace MotoBest.Services.Scraping;

public class AutoBgScraper : IScraper
{
    public AdvertScrapeModel ScrapeAdvert(IDocument document)
    {
        var remoteId = document.QuerySelector("title")?.TextContent.Split(" ")[^3];
        var title = document.QuerySelector("div.titleBig > h1")?.TextContent;
        var description = document.QuerySelector("div.moreInfo")?.TextContent;

        return new AdvertScrapeModel
        {
            RemoteId = remoteId,
            Title = title,
            Description = description
        };
    }
}
