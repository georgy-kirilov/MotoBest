using AngleSharp;

using Microsoft.Extensions.Hosting;

using MotoBest.Common;
using MotoBest.Services.Normalizing;
using System.Text;

namespace MotoBest.Services.Scraping;

public class ScrapingBackgroundService : BackgroundService
{
    private readonly IScraper scraper;
    private readonly INormalizer normalizer;

    public ScrapingBackgroundService(IScraper scraper, INormalizer normalizer)
    {
        this.scraper = scraper;
        this.normalizer = normalizer;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.OutputEncoding = Encoding.Unicode;
        int delayMilliseconds = 1_000 * 12;

        var config = Configuration.Default.WithDefaultLoader();
        var context = BrowsingContext.New(config);

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(delayMilliseconds, stoppingToken);

            var latestModifiedOnDate = DateTime.Now.Subtract(TimeSpan.FromHours(1));
            int resultsPageIndex = 1;

            while (true)
            {
                var tasks = new List<Task>();
                string advertResultsUrl = $"https://www.auto.bg/obiavi/avtomobili-dzhipove/page/{resultsPageIndex}?nup=013&searchres=7ssg2pp1&sort=1";
                var advertResultsPageDocument = await context.OpenAsync(advertResultsUrl, stoppingToken);

                var advertResults = scraper
                    .ScrapeAdvertResultsFromPage(advertResultsPageDocument)
                    .Where(res => res != null && res.ModifiedOn >= latestModifiedOnDate);

                if (!advertResults.Any())
                {
                    break;
                }

                foreach (var advertResult in advertResults)
                {
                    async Task method()
                    {
                        var fullAdvertDocument = await context.OpenAsync(advertResult!.Url, stoppingToken);
                        var scrapedAdvert = scraper.ScrapeAdvert(fullAdvertDocument);
                        scrapedAdvert.ModifiedOn = advertResult.ModifiedOn;
                        var normalizedAdvert = normalizer.Normalize(scrapedAdvert);
                        Console.WriteLine(normalizedAdvert.ToJson());
                    }

                    var task = Task.Run(method, stoppingToken);
                    tasks.Add(task);
                }

                Task.WaitAll(tasks.ToArray(), stoppingToken);
                resultsPageIndex++;
            }
        }
    }
}
