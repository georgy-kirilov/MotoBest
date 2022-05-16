using MotoBest.Services.Normalization;
using MotoBest.Services.Data.Adverts.Models;

using static MotoBest.Services.Data.Adverts.AdvertServiceConstants;

namespace MotoBest.Services.Data.Adverts;

public interface IAdvertService
{
    Task AddOrUpdate(NormalizedAdvert advert);

    DateTime? FindLatestAdvertModifiedOnDate(string site);

    IEnumerable<SearchAdvertsResultModel> SearchAdverts(
        SearchAdvertsInputModel input,
        int pageIndex = SearchResultsFirstPageIndex,
        int resultsPerPageCount = SearchResultsPerPageCount);

    Task<GetFullAdvertResultModel?> GetFullAdvert(string id);
}
