using MotoBest.Services.Normalization;

namespace MotoBest.Services.Data.Adverts;

public interface IAdvertService
{
    Task AddOrUpdateAsync(NormalizedAdvert advert);

    DateTime? GetLatestAdvertModifiedOnDate(string site);

    IEnumerable<AdvertSearchResult> SearchAdverts(
        AdvertSearchFilter filter,
        int pageIndex = AdvertServiceConstants.AdvertSearchResultsFirstPageIndex,
        int resultsPerPageCount = AdvertServiceConstants.AdvertSearchResultsPerPageCount);
}
