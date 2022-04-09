using AngleSharp;

using System.Net;
using System.Text;

using MotoBest.Common;

using MotoBest.Services.Common;
using MotoBest.Services.Scraping.DesktopAutoBg;

using MotoBest.Data.Seeding.Dtos;
using MotoBest.Data.Seeding.Constants;

using static MotoBest.Common.GlobalConstants;
using System.Text.Json;

Console.OutputEncoding = Encoding.UTF8;

var siteScraper = new DesktopAutoBgSiteScraper(new DateTimeManager());

var browsingContext = BrowsingContext.New(Configuration.Default.WithDefaultLoader());

var brandDtos = new List<BrandDto>();

foreach (string brand in typeof(BrandNames).GetAllPublicConstantValues<string>())
{
    string encodedBrand = brand.ToLower().Replace(Whitespace, "-");
    string url = $"https://www.auto.bg/obiavi/avtomobili-dzhipove/{encodedBrand}";

    var document = await browsingContext.OpenAsync(url);

    if (document.StatusCode != HttpStatusCode.OK)
    {
        continue;
    }

    var modelDtos = siteScraper.ScrapeAllModels(document).Select(m => new ModelDto(m));

    if (!modelDtos.Any())
    {
        continue;
    }

    brandDtos.Add(new BrandDto(brand, modelDtos));
}


string filePath = $"./Output/models-by-brands-{Guid.NewGuid()}.json";

new FileInfo(filePath)?.Directory?.Create();
await File.WriteAllTextAsync(filePath, brandDtos.ToJson());
