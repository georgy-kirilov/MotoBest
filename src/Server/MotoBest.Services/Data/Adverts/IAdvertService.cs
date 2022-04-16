using MotoBest.Services.Normalization;

namespace MotoBest.Services.Data.Adverts;

public interface IAdvertService
{
    Task AddAsync(NormalizedAdvert advert);

    Task<DateTime?> LatestAdvertModifiedOnDateAsync(string site);

    Task<IEnumerable<SearchAdvertResult>> SearchAdvertsAsync(
        SearchAdvertsFilter filter,
        int pageIndex = Constants.SearchAdvertResultsFirstPageIndex,
        int resultsPerPageCount = Constants.SearchAdvertResultsPerPageCount);
}
