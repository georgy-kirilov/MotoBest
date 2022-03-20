using AngleSharp;
using Microsoft.Extensions.Hosting;
using MotoBest.Common;
using System.Text;

namespace MotoBest.Services.Scraping;

public class ScrapingBackgroundService : BackgroundService
{
    private readonly IScraper scraper;

    public ScrapingBackgroundService(IScraper scraper)
    {
        this.scraper = scraper;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.OutputEncoding = Encoding.Unicode;
        int delayMilliseconds = 1_000 * 10;

        var config = Configuration.Default.WithDefaultLoader();
        var context = BrowsingContext.New(config);

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(delayMilliseconds, stoppingToken);

            var latestModifiedOnDate = new DateTime(2022, 3, 20, 12, 0, 0);

            int resultsPageIndex = 1;

            while (true)
            {
                var tasks = new List<Task>();

                var document = await context.OpenAsync(
                    $"https://www.auto.bg/obiavi/avtomobili-dzhipove/page/{resultsPageIndex}?nup=013&searchres=7ssg2pp1&sort=1");

                var advertResults = scraper
                    .ScrapeAdvertResultsFromPage(document)
                    .Where(res => res != null && res.ModifiedOn > latestModifiedOnDate);

                if (!advertResults.Any())
                {
                    break;
                }

                foreach (var advertResult in advertResults)
                {
                    var task = Task.Run(async () =>
                    {
                        var scrapeModel = scraper.ScrapeAdvert(
                            await context.OpenAsync(advertResult!.Url));

                        Console.WriteLine(scrapeModel.ToJson());
                    });

                    tasks.Add(task);
                }

                Task.WaitAll(tasks.ToArray());
                Console.WriteLine($"{resultsPageIndex} -> {advertResults.Count()}");
                resultsPageIndex++;
            }

        }
    }
}
