using AngleSharp;
using Microsoft.Extensions.Hosting;

namespace MotoBest.Services.Scraping;

public class ScrapingHostedService : IHostedService, IDisposable
{
    private readonly IScraper scraper;
    private Timer timer = default!;

    public ScrapingHostedService(IScraper scraper)
    {
        this.scraper = scraper;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        timer?.Dispose();
        GC.SuppressFinalize(this);
    }

    private async void DoWork(object? state)
    {
        var config = Configuration.Default.WithDefaultLoader();
        var context = BrowsingContext.New(config);
        var document = await context.OpenAsync("https://www.auto.bg/obiava/63182274/audi-a6-3-0tfsi-matrix-3xs-line-keyless-head-up?page=1&searchres=qhs3qeo1");
        var advert = scraper.ScrapeAdvert(document);
        Console.WriteLine(advert.Title);
    }
}
