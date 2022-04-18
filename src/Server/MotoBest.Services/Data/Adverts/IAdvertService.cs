using MotoBest.Services.Normalization;
using MotoBest.Services.Data.Adverts.Models;

namespace MotoBest.Services.Data.Adverts;

public interface IAdvertService
{
    Task AddOrUpdateAsync(NormalizedAdvert advert);

    DateTime? GetLatestAdvertModifiedOnDate(string site);

    IEnumerable<AdvertSearchResultDto> SearchAdverts(
        AdvertSearchFilterDto filter,
        int pageIndex = AdvertServiceConstants.AdvertSearchResultsFirstPageIndex,
        int resultsPerPageCount = AdvertServiceConstants.AdvertSearchResultsPerPageCount);

    Task<FullAdvertDto?> GetFullAdvertAsync(string id);
}
