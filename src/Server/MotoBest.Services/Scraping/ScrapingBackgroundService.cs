using AngleSharp;

using System.Text;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using MotoBest.Services.Normalization;
using MotoBest.Services.Scraping.Common;
using MotoBest.Data.Seeding.Constants;
using MotoBest.Services.Data.Adverts;

namespace MotoBest.Services.Scraping;

public class ScrapingBackgroundService : BackgroundService
{
    private readonly ISiteScraper scraper;
    private readonly ISiteDataNormalizer normalizer;
    private readonly IServiceScopeFactory serviceScopeFactory;

    public ScrapingBackgroundService(
        ISiteScraper scraper,
        ISiteDataNormalizer normalizer,
        IServiceScopeFactory serviceScopeFactory)
    {
        this.scraper = scraper;
        this.normalizer = normalizer;
        this.serviceScopeFactory = serviceScopeFactory;
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

            using var scope = serviceScopeFactory.CreateScope();
            var latestModifiedOnDate = scope.ServiceProvider.GetRequiredService<IAdvertService>()
                .GetLatestAdvertModifiedOnDate(SiteNames.AutoBg) ?? DateTime.Now.Subtract(TimeSpan.FromHours(1));

            int resultsPageIndex = 1;

            while (true)
            {
                var tasks = new List<Task>();
                string advertResultsUrl = $"https://www.auto.bg/obiavi/avtomobili-dzhipove/page/{resultsPageIndex}?nup=013&searchres=7ssg2pp1&sort=1";
                var advertResultsPageDocument = await context.OpenAsync(advertResultsUrl, stoppingToken);

                var advertResults = scraper
                    .ScrapeSearchAdvertResults(advertResultsPageDocument)
                    .Where(res => res != null && res.ModifiedOn >= latestModifiedOnDate);

                if (!advertResults.Any())
                {
                    break;
                }

                foreach (var advertResult in advertResults)
                {
                    async Task method()
                    {
                        using var scope = serviceScopeFactory.CreateScope();
                        var advertService = scope.ServiceProvider.GetRequiredService<IAdvertService>();
                        var fullAdvertDocument = await context.OpenAsync(advertResult!.Url, stoppingToken);
                        var scrapedAdvert = scraper.ScrapeAdvert(fullAdvertDocument);
                        scrapedAdvert.ModifiedOn = advertResult.ModifiedOn;
                        var normalizedAdvert = normalizer.NormalizeAdvert(scrapedAdvert);
                        await advertService.AddOrUpdateAsync(normalizedAdvert);
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
