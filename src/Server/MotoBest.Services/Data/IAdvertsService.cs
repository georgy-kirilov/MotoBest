using MotoBest.Services.Normalizing;

namespace MotoBest.Services.Data;

public interface IAdvertsService
{
    Task AddAsync(NormalizedAdvert advert);
}
