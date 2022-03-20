using AngleSharp;
using Microsoft.Extensions.Hosting;
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
        int delayMilliseconds = 1_000 * 5;

        var config = Configuration.Default.WithDefaultLoader();
        var context = BrowsingContext.New(config);

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(delayMilliseconds, stoppingToken);

            var document = await context.OpenAsync("https://www.auto.bg/obiavi/avtomobili-dzhipove");
            var urls = scraper.ScrapeAdvertsUrlsFromPage(document);

            var tasks = new List<Task>();

            foreach (string url in urls)
            {
                var task = Task.Run(async () =>
                {
                    var scrapeModel = scraper.ScrapeAdvert(await context.OpenAsync(url));
                });

                tasks.Add(task);
            }

            Task.WaitAll(tasks.ToArray());
        }
    }
}
